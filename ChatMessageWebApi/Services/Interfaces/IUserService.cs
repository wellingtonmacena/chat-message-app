using ChatMessageWebApi.Models.DTOs;

namespace ChatMessageWebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> Login(string email, string password);
    }
}
