using ChatMessageWebApi.Models.DTOs;
using posterr_webapi.Models.DTOs;

namespace posterr_webapi.src.Models.DTOs
{
    public class SharedPostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsARepost { get; set; }
        public int? RepostCount { get; set; }
        public string PostCreatorUsername { get; set; }
        public   UserDto PostCreator { get; set; }
        public string ReposterUsername { get; set; }
        public Guid? PostId { get; set; }
        public   PostDto OriginalPost { get; set; }

    }
}
