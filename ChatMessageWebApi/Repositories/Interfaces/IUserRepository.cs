using ChatMessageWebApi.Models.Entities;
using ChatMessageWebApi.Models.Requests;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserBy(string email, string password);
        Task<Paginate<Message>> GetMessages(GetMessagesRequest getMessagesRequest);
        Task<Message> CreateMessage(PostNewMessageRequest message);
    }
}
