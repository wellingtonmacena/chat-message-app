using ChatMessageWebApi.Models.Entities;

namespace ChatMessageWebApi.Models.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid ConversationId { get; set; }
    }
}
