using BloggingAPI.Infrastructure.Entities;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public interface ITagsRepository : IRepository<Tag>
    {
        /// <summary>
        /// Returns a tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<Tag> GetTagAsync(string tag);
    }
}
