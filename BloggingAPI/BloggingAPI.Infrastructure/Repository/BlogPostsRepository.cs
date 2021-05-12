using BloggingAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public class BlogPostsRepository : Repository<BlogPost>, IBlogPostsRepository
    {
        public BlogPostsRepository(BloggingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BlogPost>> GetBlogPosts()
        {
            return await GetAll()
                            .Include(bp => bp.Tags)
                            .OrderByDescending(e => e.CreatedAt)
                            .ToListAsync()
                            .ConfigureAwait(false);
        }

        public async Task<List<BlogPost>> GetBlogPostsByTagAsync(Tag tag)
        {
            return await GetAll()
                            .Include(bp => bp.Tags)
                            .Where(e => e.Tags.Contains(tag))
                            .OrderByDescending(e => e.CreatedAt)
                            .ToListAsync()
                            .ConfigureAwait(false);
        }

        public async Task<BlogPost> GetBlogPostBySlugAsync(string slug)
        {
            return await GetAll()
                            .Include(entity => entity.Tags)
                            .FirstOrDefaultAsync(entity => entity.Slug == slug)
                            .ConfigureAwait(false);
        }
    }
}
