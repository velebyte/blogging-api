using BloggingAPI.Domain;
using BloggingAPI.Domain.Mappers;
using BloggingAPI.Infrastructure.Entities;
using BloggingAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.ApplicationCore
{
    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> _tagsRepository;
        public TagsService(IRepository<Tag> tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public async Task<List<string>> GetTagsAsync()
        {
            var tags = await _tagsRepository.GetAll()
                                            .ToListAsync()
                                            .ConfigureAwait(false);

            return tags.ToStringList();
        }
    }
}
