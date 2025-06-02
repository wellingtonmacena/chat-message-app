using ChatMessageWebApi.Models.Entities;

namespace ChatMessageWebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserBy(string email, string password);

    }
}
