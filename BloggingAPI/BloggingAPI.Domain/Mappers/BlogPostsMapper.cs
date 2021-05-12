using BloggingAPI.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BloggingAPI.Domain.Mappers
{
    public static class BlogPostsMapper
    {
        #region ToDTOs
        /// <summary>
        /// Maps a Blog Post entity to a Blog Post DTO
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static BlogPostPayload ToDTO(this BlogPost post)
        {
            return post == null ?
                null :
                new BlogPostPayload()
                {
                    Title = post.Title,
                    Description = post.Description,
                    Body = post.Body,
                    TagList = post.Tags.ToDTO(),
                    CreatedAt = post.CreatedAt,
                    Slug = post.Slug,
                    UpdatedAt = post.UpdatedAt != null ? post.UpdatedAt.Value : null
                };
        }

        /// <summary>
        /// Maps a list of Blog Posts to a List of Blog Post DTOs
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static List<BlogPostPayload> ToDTO(this List<BlogPost> posts)
        {
            var postsDto = new List<BlogPostPayload>();

            if (posts.Count > 0)
            {
                foreach (var post in posts)
                {
                    postsDto.Add(new BlogPostPayload
                    {
                        Title = post.Title,
                        Description = post.Description,
                        Body = post.Body,
                        Slug = post.Slug,
                        TagList = post.Tags.ToDTO(),
                        CreatedAt = post.CreatedAt,
                        UpdatedAt = post.UpdatedAt != null ? post.UpdatedAt.Value : null
                    });
                }
            }

            return postsDto;
        }

        /// <summary>
        /// Maps a Tag entity collection to a string enumerable list
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToDTO(this ICollection<Tag> tags)
        {
            var tagsList = new List<string>();

            if (tags != null && tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    if (tag != null && !string.IsNullOrEmpty(tag.Name))
                        tagsList.Add(tag.Name);
                }
            }

            return tagsList;
        }
        #endregion

        #region ToEntities
        /// <summary>
        /// Maps a Blog Post DTO to a Blog Post entity
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns></returns>
        public static BlogPost ToEntity(this BlogPostCreatePayload postDto)
        {
            return postDto == null ?
                null :
                new BlogPost()
                {
                    Title = postDto.Title,
                    Description = postDto.Description,
                    Body = postDto.Body,
                    Tags = postDto.TagList.ToEntity()
                };
        }

        /// <summary>
        /// Maps an enumerable string list to a collection of Tags
        /// </summary>
        /// <param name="tagList"></param>
        /// <returns></returns>
        public static ICollection<Tag> ToEntity(this IEnumerable<string> tagList)
        {
            var tags = new List<Tag>();

            if (tagList != null && tagList.Count() > 0)
            {
                foreach (var tag in tagList)
                {
                    if (!string.IsNullOrEmpty(tag))
                        tags.Add(new Tag { Name = tag });
                }
            }

            return tags;
        }
        #endregion
    }
}
