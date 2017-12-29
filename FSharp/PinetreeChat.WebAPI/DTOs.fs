namespace PinetreeChat.WebAPI

module DTOs =
    open Giraffe
    open Microsoft.AspNetCore.Http

    [<CLIMutable>]
    type UserDTO = { Username: string }

    [<CLIMutable>]
    type LeaveDTO = { ChatName : string;
                      Participant : string }

    [<CLIMutable>]
    type MessageDTO = { ChatName : string
                        Order : int
                        From : string
                        Text : string }

    [<CLIMutable>]
    type ChatDTO = { Name : string
                     Participants : string list
                     Messages : MessageDTO list }

    let getDto<'TDto> (ctx:HttpContext) =  ctx.BindJsonAsync<'TDto>()
        