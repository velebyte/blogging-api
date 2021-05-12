using System.ComponentModel.DataAnnotations;

namespace BloggingAPI.Domain
{
    public class BlogPostUpdatePayload
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string Body { get; set; }
    }
}
