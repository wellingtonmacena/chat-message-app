namespace ChatMessageWebApi.Models.Requests
{
    public class PostNewMessageRequest
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }

        // Foreign key for the conversation
        public Guid ConversationId { get; set; }
    }
}
