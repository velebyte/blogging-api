using BloggingAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.ApplicationCore
{
    public interface ITagsService
    {
        /// <summary>
        /// Gets all the tags
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetTagsAsync();
    }
}
