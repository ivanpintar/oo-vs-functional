using Microsoft.AspNetCore.SignalR;
using PinetreeChat.WebAPI.DTOs;

namespace PinetreeChat.WebAPI.SignalR
{
    public class ChatHub : Hub
    {
        
    }

    public class ChatHubHelper
    {
        private IHubContext<ChatHub> _chatHub;

        public ChatHubHelper(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }       

        public void ChatCreated(ChatDTO chatDTO)
        {
            InvokeAllClients("ChatCreated", chatDTO);
        }

        public void MessageSent(MessageDTO messageDTO)
        {
            InvokeAllClients("MessageSent", messageDTO);
        }

        public void ChatLeft(LeaveDTO leaveDTO)
        {
            InvokeAllClients("ChatLeft", leaveDTO);
        }

        private void InvokeAllClients(string method, object payload)
        {
            _chatHub.Clients.All.InvokeAsync(method, payload);
        }
    }
}
