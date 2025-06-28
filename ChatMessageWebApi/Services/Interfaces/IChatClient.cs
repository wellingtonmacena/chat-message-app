using ChatMessageWebApi.Models.Requests;

namespace ChatMessageWebApi.Services.Interfaces
{
    public interface IChatClient
    {
       
        Task ReceiveMessage( string message);

        Task SendMessage(string message);
        Task RemoveUser(string userId);
    }
}
