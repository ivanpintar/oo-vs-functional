namespace PinetreeChat.WebAPI

module ChatAPI =
    open Giraffe
    open Microsoft.AspNetCore.Http
    open DTOs
    open PinetreeChat.Domain.ChatService
    open PinetreeChat.Domain.ChatServiceWithDataAccess
    open Chessie.ErrorHandling
    open SignalR

    let processServiceResult (result:Result<_, ChatErrorMessage>)  =
        match result with
        | Ok _ -> setStatusCode 200
        | Bad errors -> 
            let message = errors |> List.map getErrorMessage |> String.concat ", "
            let checkError =
                try 
                    match List.head errors with
                    | ChatNameInvalid _ -> setStatusCode 400
                    | MessageInvalid _ -> setStatusCode 400
                    | ChatExists _ -> setStatusCode 409
                with _ -> setStatusCode 500
            checkError >=> json message  

    let sendNotification notificationFunction ctx payload =
        payload 
        |> notificationFunction ctx 
        |> ignore
        payload

    let listChats next ctx = 
        let res = getChats () |> List.map toChatDto
        json res next ctx

    let getChat chatName = 
        match getChat chatName with 
        | Some chat -> json (toChatDto chat)
        | None -> json null

    let createChat next (ctx:HttpContext) = 
        task {
            let! chatDto = ctx.BindJsonAsync<ChatDTO>()
            let serviceResult = 
                createChat chatDto.Name
                |> lift toChatDto
                |> lift (sendNotification chatCreated ctx)
                |> processServiceResult
                
            return! serviceResult next ctx
        }

    let sendMessage next (ctx:HttpContext) = 
        task {
            let! messageDto = ctx.BindJsonAsync<MessageDTO>()            
            let serviceResult = 
                addMessage messageDto.ChatName messageDto.Text messageDto.From 
                |> lift (fun (c, m) -> toMessageDto c m)
                |> lift (sendNotification messageSent ctx)
                |> processServiceResult

            return! serviceResult next ctx
        }

    let leaveChat next (ctx:HttpContext) = 
        task {
            let! leaveDto = ctx.BindJsonAsync<LeaveDTO>()
            let serviceResult = 
                leaveChat leaveDto.ChatName leaveDto.Participant
                |> lift (fun(r) -> sendNotification chatLeft ctx leaveDto |> ok)
                |> processServiceResult

            return! serviceResult next ctx
        }

    let routeHandler httpFunc httpContext = 
        let routes = [
            GET >=> choose [
                route "/list" >=> listChats
                routef "/%s" getChat 
            ]
            POST >=> choose [
                route "/create" >=> createChat
                route "/sendMessage" >=> sendMessage
                route "/leave" >=> leaveChat
            ]
        ]
        choose routes httpFunc httpContext
