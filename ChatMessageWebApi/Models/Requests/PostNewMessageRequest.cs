using System.Text.Json.Serialization;

namespace ChatMessageWebApi.Models.Requests
{
    public class PostNewMessageRequest
    {
        [JsonPropertyName("id")]
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }

        // Foreign key for the conversation
        public Guid ConversationId { get; set; }
    }
}
