using Microsoft.AspNetCore.SignalR;
using PinetreeChat.WebAPI.DTOs;
using System.Threading.Tasks;

namespace PinetreeChat.WebAPI.SignalR
{
    public class ChatHub : Hub
    {
        public Task ChatCreated(ChatDTO chatDto)
        {
            return Clients.All.InvokeAsync("ChatCreated", chatDto);
        }

        public Task MessageSent(MessageDTO messageDto)
        {
            return Clients.All.InvokeAsync("MessageSent", messageDto);
        }

        public Task ChatLeft(LeaveDTO leaveDto)
        {
            return Clients.All.InvokeAsync("ChatLeft", leaveDto);
        }
    }
}
