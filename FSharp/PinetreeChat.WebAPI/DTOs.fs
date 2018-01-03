namespace PinetreeChat.WebAPI

module DTOs =
    open Giraffe
    open Microsoft.AspNetCore.Http
    open PinetreeChat.Entities

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

    let toUserDto (ue:User) = { Username = ue.Username }
    let toMessageDto (c:Chat) (m:Message) = { ChatName = c.Name
                                              Order = m.Order
                                              From = m.From.Username
                                              Text = m.Text }
    let toChatDto (c:Chat) = { Name = c.Name
                               Participants = List.map (fun (u:User) -> u.Username) c.Participants
                               Messages = List.map (toMessageDto c) c.Messages }