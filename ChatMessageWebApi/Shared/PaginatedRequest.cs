using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;

namespace posterr_webapi.src.Shared
{
    public class PaginatedRequest
    {
        [DefaultValue(0)]
        public int PageNumber { get; set; }
        [DefaultValue(15)]
        public int PageSize { get; set; }
    }
}
