namespace PinetreeChat.WebAPI
 
module SignalR = 
    open Microsoft.AspNetCore.SignalR

    type ChatHub() = inherit Hub()

    let invokeAllClients (chatHubContext:IHubContext<ChatHub>) method payload = 
        chatHubContext.Clients.All.InvokeAsync(method, payload)

    let chatCreated (chatHubContext:IHubContext<ChatHub>) = invokeAllClients chatHubContext "ChatCreated"        
    let messageSent (chatHubContext:IHubContext<ChatHub>) = invokeAllClients chatHubContext "MessageSent"         
    let chatLeft (chatHubContext:IHubContext<ChatHub>) = invokeAllClients chatHubContext "ChatLeft" 


