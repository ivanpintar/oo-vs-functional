namespace PinetreeChat.WebAPI


module UserAPI =
    open Giraffe
    open DTOs
    open Microsoft.AspNetCore.Http
    open PinetreeChat.Domain.UserService
    open PinetreeChat.Domain.UserServiceWithDataAccess
    open Chessie.ErrorHandling
    open System

    let processServiceResult (result:Result<_, UserErrorMessage>)  =
        match result with
        | Ok (r, _) -> setStatusCode 200
        | Bad errors -> 
            let message = errors |> List.map getErrorMessage |> String.concat ", "
            let checkError =
                try 
                    match List.head errors with
                    | UsernameInvalid _ -> setStatusCode 400
                    | UserLoggedIn _ -> setStatusCode 409
                with _ -> setStatusCode 500
            checkError >=> json message         

    let loginUser next ctx =
        task {
            let! userDto = getDto<UserDTO> ctx
            let loginUserResult = 
                userDto.Username
                |> login
                |> processServiceResult
            
            return! loginUserResult next ctx
        }

    let logoutUser next ctx =
        task {
            let! userDto = getDto<UserDTO> ctx
            let logOutUserResult = 
                userDto.Username
                |> logout
                |> processServiceResult
            return! logOutUserResult next ctx
        }

    let routeHandler httpFunc httpContext = 
        let routes = [
            POST >=>
                choose [
                    route "/login" >=> loginUser                     
                    route "/logout" >=> logoutUser
                ]
           ]
        choose routes httpFunc httpContext


