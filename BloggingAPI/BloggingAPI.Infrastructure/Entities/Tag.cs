using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloggingAPI.Infrastructure.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
