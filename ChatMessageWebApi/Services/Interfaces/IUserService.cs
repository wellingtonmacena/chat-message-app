using ChatMessageWebApi.Models.DTOs;
using ChatMessageWebApi.Models.Requests;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> Login(string email, string password);

        Task<MessageDto> CreateMessage(PostNewMessageRequest postNewMessageRequest);
        Task<Paginate<MessageDto>> GetMessages(GetMessagesRequest getMessagesRequest);
    }
}
