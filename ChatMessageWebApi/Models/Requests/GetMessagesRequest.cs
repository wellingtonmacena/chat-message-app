using posterr_webapi.src.Shared;
using System.ComponentModel.DataAnnotations;

namespace ChatMessageWebApi.Models.Requests
{
    public class GetMessagesRequest:PaginatedRequest
    {
        [Required]
        public Guid RecipientId { get; set; }
        [Required]
        public Guid SenderId { get; set; }
    }
}
