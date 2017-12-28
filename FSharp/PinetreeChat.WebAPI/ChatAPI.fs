namespace PinetreeChat.WebAPI

module ChatAPI =
    open Giraffe
    open Microsoft.AspNetCore.Http
    open DTOs

    let listChats = json [] 

    let getChat chatName = 
        let chat = { Name = chatName; Participants = []; Messages = [] }
        json chat

    let createChat next (ctx:HttpContext) = 
        task {
            let! chatDto = ctx.BindJsonAsync<ChatDTO>()
            let result = { chatDto with Messages = []; Participants = [] }
            return! json result next ctx
        }

    let sendMessage next (ctx:HttpContext) = 
        task {
            let! messageDto = ctx.BindJsonAsync<MessageDTO>()
            return! json messageDto next ctx
        }

    let leaveChat next (ctx:HttpContext) = 
        task {
            let! leaveDto = ctx.BindJsonAsync<LeaveDTO>()
            return! json leaveDto next ctx
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
