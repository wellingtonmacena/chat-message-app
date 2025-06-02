using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatMessageWebApi.Models.Entities
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        [ForeignKey(nameof(RecipientId))]
        public User Recipient { get; set; }

        // Navigation property for related messages
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
