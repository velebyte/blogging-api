using BloggingAPI.Domain;
using BloggingAPI.Domain.Mappers;
using BloggingAPI.Infrastructure.Entities;
using BloggingAPI.Infrastructure.Repository;
using Slugify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.ApplicationCore
{
    public class PostsService : IPostsService
    {
        private readonly IBlogPostsRepository _blogPostRepository;
        private readonly ITagsRepository _tagsRepository;
        private readonly SlugHelper _slugHelper;
        public PostsService(IBlogPostsRepository blogPostRepository,
                            ITagsRepository tagsRepository,
                            SlugHelper slugHelper)
        {
            _blogPostRepository = blogPostRepository;
            _tagsRepository = tagsRepository;
            _slugHelper = slugHelper;
        }

        public async Task<BlogPostPayload> GetBlogPostBySlugAsync(string slug)
        {
            var blogPostEntity = await _blogPostRepository.GetBlogPostBySlugAsync(slug).ConfigureAwait(false);

            if (blogPostEntity != null)
                return blogPostEntity.ToDTO();

            return null;
        }

        public async Task<List<BlogPostPayload>> GetBlogPostsAsync(string tag)
        {
            List<BlogPost> blogPosts;

            if (!string.IsNullOrEmpty(tag))
            {
                var tagEntity = await _tagsRepository.GetTagAsync(tag);
                blogPosts = await _blogPostRepository.GetBlogPostsByTagAsync(tagEntity);
            }
            else
                blogPosts = await _blogPostRepository.GetBlogPosts();

            return blogPosts.ToDTO();
        }

        public async Task<BlogPostPayload> CreateBlogPostAsync(BlogPostCreatePayload blogPostPayload)
        {
            var blogPostEntity = blogPostPayload.ToEntity();
            blogPostEntity.Tags.Clear();

            foreach (var tag in blogPostPayload.TagList)
            {
                var preexistingTag = await _tagsRepository.GetTagAsync(tag);
                if (preexistingTag == null)
                    preexistingTag = new Tag { Name = tag };

                blogPostEntity.Tags.Add(preexistingTag);
            }

            blogPostEntity.Slug = _slugHelper.GenerateSlug(blogPostEntity.Title);
            blogPostEntity.CreatedAt = DateTime.Now;

            blogPostEntity = await _blogPostRepository.InsertAsync(blogPostEntity).ConfigureAwait(false);

            if (blogPostEntity != null)
                return blogPostEntity.ToDTO();

            return null;
        }

        public async Task<BlogPostPayload> UpdateBlogPostAsync(string slug, BlogPostUpdatePayload blogPostPayload)
        {
            var blogPostEntity = await _blogPostRepository.GetBlogPostBySlugAsync(slug).ConfigureAwait(false);

            if (blogPostEntity != null)
            {
                var isUpdate = false;

                if (!string.IsNullOrEmpty(blogPostPayload.Title))
                {
                    blogPostEntity.Title = blogPostPayload.Title;
                    blogPostEntity.Slug = _slugHelper.GenerateSlug(blogPostPayload.Title);
                    isUpdate = true;
                }

                if (!string.IsNullOrEmpty(blogPostPayload.Description))
                {
                    blogPostEntity.Description = blogPostPayload.Description;
                    isUpdate = true;
                }

                if (!string.IsNullOrEmpty(blogPostPayload.Body))
                {
                    blogPostEntity.Body = blogPostPayload.Body;
                    isUpdate = true;
                }

                if (isUpdate)
                {
                    blogPostEntity.UpdatedAt = DateTime.Now;
                    blogPostEntity = await _blogPostRepository.UpdateAsync(blogPostEntity).ConfigureAwait(false);
                }

                if (blogPostEntity != null)
                    return blogPostEntity.ToDTO();
            }

            return null;
        }

        public async Task<bool> DeleteBlogPostAsync(string slug)
        {
            var blogPostEntity = await _blogPostRepository.GetBlogPostBySlugAsync(slug).ConfigureAwait(false);

            if (blogPostEntity != null)
                return await _blogPostRepository.DeleteAsync(blogPostEntity).ConfigureAwait(false);

            return false;
        }

    }
}
