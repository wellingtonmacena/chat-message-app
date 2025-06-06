using System.Text.Json.Serialization;

namespace ChatMessageWebApi.Models.Requests
{
    public class PostNewMessageRequest
    {
        [JsonPropertyName("id")]
        public Guid MessageId { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("senderId")]
        public Guid SenderId { get; set; }
        [JsonPropertyName("recipientId")]
        public Guid RecipientId { get; set; }
    }
}
