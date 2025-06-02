using ChatMessageWebApi.Models.DTOs;

namespace posterr_webapi.Models.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsARepost { get; set; }
        public int? RepostCount { get; set; }
        public string PostCreatorUsername { get; set; }
        public UserDto PostCreator { get; set; }
    }
}
