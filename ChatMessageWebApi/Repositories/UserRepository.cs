using ChatMessageWebApi.Data;
using ChatMessageWebApi.Models.Entities;
using ChatMessageWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatMessageWebApi.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private AppDbContext _appDbContext = appDbContext;
        public async Task<User> GetUserBy(string email, string password)
        {
            return await _appDbContext.Users
                                      .AsNoTracking()
                                     .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
        }


        public async Task<List<User>> GetUsers()
        {
            return await _appDbContext.Users
                                     .AsNoTracking()
                                    .ToListAsync();
        }
    }
}