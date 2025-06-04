using System.ComponentModel.DataAnnotations;

namespace ChatMessageWebApi.Models.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Foreign key for the conversation
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }

    }
}
