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
            var conversation = await _appDbContext.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == message.ConversationId || c.RecipientId.Equals(message.RecipientId) && c.SenderId.Equals(message.SenderId));


            if (conversation == null)
            {
                conversation = new Conversation
                {
                    SenderId = message.SenderId,
                    RecipientId = message.RecipientId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,

                };

                conversation =  (await  _appDbContext.AddAsync(conversation)).Entity;
            }

            var newMessage = new Message
            {
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                ConversationId = conversation.Id,
                UpdatedAt = message.CreatedAt
            };

            await _appDbContext.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();

            return newMessage;




        }

        public async Task<Paginate<Message>> GetMessages(GetMessagesRequest getMessagesRequest)
        {
            var conversation = await _appDbContext.Conversations
                .Include(c => c.Messages)
                .FirstAsync(c => c.SenderId.Equals(getMessagesRequest.SenderId) && c.RecipientId.Equals(getMessagesRequest.RecipientId))
               ;

            int totalCount = conversation.Messages .Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / getMessagesRequest.PageSize);

            List<Message> paginatedItems = conversation.Messages

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