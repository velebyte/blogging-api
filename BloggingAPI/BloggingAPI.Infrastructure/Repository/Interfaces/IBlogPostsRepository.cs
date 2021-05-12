using BloggingAPI.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public interface IBlogPostsRepository : IRepository<BlogPost>
    {
        /// <summary>
        /// Get all latest Blog Posts 
        /// </summary>
        /// <returns></returns>
        Task<List<BlogPost>> GetBlogPosts();

        /// <summary>
        /// Get all recent blog posts filtered by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<List<BlogPost>> GetBlogPostsByTagAsync(Tag tag);

        /// <summary>
        /// Get a Blog Post by Slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<BlogPost> GetBlogPostBySlugAsync(string slug);
    }
}
