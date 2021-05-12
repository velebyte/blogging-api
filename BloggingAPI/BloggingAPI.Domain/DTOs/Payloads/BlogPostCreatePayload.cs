using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloggingAPI.Domain
{
    public class BlogPostCreatePayload
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> TagList { get; set; }
    }
}
