namespace posterr_webapi.Models.DTOs
{
    public class RepostDto : PostDto
    {
        public string ReposterUsername { get; set; }
        public Guid? PostId { get; set; }
        public   PostDto OriginalPost { get; set; }
    }
}
