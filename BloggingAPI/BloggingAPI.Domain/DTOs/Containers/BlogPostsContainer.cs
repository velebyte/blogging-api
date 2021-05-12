using System.Collections.Generic;

namespace BloggingAPI.Domain
{
    public class BlogPostsContainer
    {
        public List<BlogPostPayload> BlogPosts { get; set; }
        public int PostsCount { get; set; }
    }
}
