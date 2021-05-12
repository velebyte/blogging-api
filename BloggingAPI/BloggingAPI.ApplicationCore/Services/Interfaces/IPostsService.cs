using BloggingAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.ApplicationCore
{
    public interface IPostsService
    {
        /// <summary>
        /// Creates a Blog Post
        /// </summary>
        /// <param name="blogPostDto"></param>
        /// <returns></returns>
        Task<BlogPostPayload> CreateBlogPostAsync(BlogPostCreatePayload blogPostPayload);

        /// <summary>
        /// Updates a Blog Post
        /// </summary>
        /// <param name="blogPostDto"></param>
        /// <returns></returns>
        Task<BlogPostPayload> UpdateBlogPostAsync(string slug, BlogPostUpdatePayload blogPostPayload);

        /// <summary>
        /// Deletes a Blog Post
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<bool> DeleteBlogPostAsync(string slug);

        /// <summary>
        /// Gets a Blog Post by Slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<BlogPostPayload> GetBlogPostBySlugAsync(string slug);

        /// <summary>
        /// Gets all recent blog posts and optionally filters by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<List<BlogPostPayload>> GetBlogPostsAsync(string tag);
    }
}
