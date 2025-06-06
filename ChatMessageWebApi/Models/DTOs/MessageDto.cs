using System.Text.Json.Serialization;

namespace ChatMessageWebApi.Models.DTOs
{
    public class MessageDto
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
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
