using BloggingAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public class TagsRepository : Repository<Tag>, ITagsRepository
    {
        public TagsRepository(BloggingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Tag> GetTagAsync(string tag)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Name == tag);
        }
    }
}
