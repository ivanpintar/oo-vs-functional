namespace PinetreeChat.WebAPI
 
module SignalR = 
    open Microsoft.AspNetCore.SignalR
    open Microsoft.AspNetCore.Http

    type ChatHub() = inherit Hub()

    let invokeAllClients (ctx:HttpContext) method (payload:obj) = 
        let chatHubContext = ctx.RequestServices.GetService(typeof<IHubContext<ChatHub>>) :?> IHubContext<ChatHub>
        chatHubContext.Clients.All.InvokeAsync(method, payload)

    let chatCreated ctx = invokeAllClients ctx "ChatCreated"        
    let messageSent ctx = invokeAllClients ctx "MessageSent"         
    let chatLeft ctx = invokeAllClients ctx "ChatLeft" 


