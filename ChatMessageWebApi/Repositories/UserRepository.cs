using ChatMessageWebApi.Data;
using ChatMessageWebApi.Models.Entities;
using ChatMessageWebApi.Models.Requests;
using ChatMessageWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using posterr_webapi.src.Shared;

namespace ChatMessageWebApi.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private AppDbContext _appDbContext = appDbContext;

        public async Task<Message> CreateMessage(PostNewMessageRequest message)
        {

            Message newMessage = new()
            {
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                RecipientId = message.RecipientId,
                SenderId = message.SenderId,
                UpdatedAt = message.CreatedAt,

            };

            await _appDbContext.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();

            return newMessage;

        }

        public async Task<Paginate<Message>> GetMessages(GetMessagesRequest getMessagesRequest)
        {
            List<Message> messages = await _appDbContext.Messages
                 .Where(c => c.SenderId.Equals(getMessagesRequest.SenderId) && c.RecipientId.Equals(getMessagesRequest.RecipientId)
                                     || c.SenderId.Equals(getMessagesRequest.RecipientId) && c.RecipientId.Equals(getMessagesRequest.SenderId)).ToListAsync();


            int totalCount = messages.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / getMessagesRequest.PageSize);

            List<Message> paginatedItems = messages

                .Skip((getMessagesRequest.PageNumber - 1) * getMessagesRequest.PageSize)
                .Take(getMessagesRequest.PageSize)
                .ToList();



            return new Paginate<Message>(paginatedItems, totalCount, getMessagesRequest.PageSize, getMessagesRequest.PageNumber, totalPages);
        }

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