namespace PinetreeChat.WebAPI


module UserAPI =
    open Giraffe
    open DTOs
    open Microsoft.AspNetCore.Http

    let loginUser next (ctx:HttpContext) =
        task {
            let! userDto = ctx.BindJsonAsync<UserDTO>()
            return! json userDto next ctx
        }

    let logoutUser next (ctx:HttpContext) =
        task {
            let! userDto = ctx.BindJsonAsync<UserDTO>()
            return! json userDto next ctx
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


