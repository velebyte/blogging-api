using BloggingAPI.ApplicationCore;
using BloggingAPI.Domain;
using BloggingAPI.Domain.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BloggingAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Produces("application/json")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        public BlogPostsController(
            IPostsService postsService)
        {
            _postsService = postsService;
        }

        /// <summary>
        /// Gets all latest blog posts and optionally filters them by a tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        // GET api/<PostsController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(BlogPostsContainer))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No Content")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult<BlogPostsContainer>> GetBlogPostsAsync([FromQuery] string tag)
        {
            try
            {
                var blogPosts = await _postsService.GetBlogPostsAsync(tag);

                if (blogPosts.Count == 0)
                    return NoContent();

                return Ok(new BlogPostsContainer { BlogPosts = blogPosts, PostsCount = blogPosts.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Returns a specific blog post
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        // GET: api/<PostsController>/:slug
        [HttpGet("{slug}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(BlogPostContainer<BlogPostPayload>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No Content")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult<BlogPostContainer<BlogPostPayload>>> GetBlogPostBySlugAsync(string slug)
        {
            try
            {
                if (string.IsNullOrEmpty(slug))
                    return BadRequest();

                var blogPostPayload = await _postsService.GetBlogPostBySlugAsync(slug).ConfigureAwait(false);

                if (blogPostPayload == null)
                    return NoContent();

                return Ok(new BlogPostContainer<BlogPostPayload> { BlogPost = blogPostPayload });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a blog post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "blogPost": {
        ///             "title": "Internet Trends 2018",
        ///             "description": "Ever wonder how?",
        ///             "body": "An opinionated commentary, of the most important presentation of the year",
        ///             "tagList": ["trends", "innovation", "2018"]
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <param name="requestPayload"></param>
        /// <returns></returns>
        // POST api/<PostsController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Created", Type = typeof(BlogPostContainer<BlogPostPayload>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Create Failed", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult<BlogPostContainer<BlogPostPayload>>> CreateBlogPostAsync([FromBody] BlogPostContainer<BlogPostCreatePayload> requestPayload)
        {
            try
            {
                if ((requestPayload == null || requestPayload.BlogPost == null) && !ModelState.IsValid)
                    return BadRequest();

                var blogPost = await _postsService.CreateBlogPostAsync(requestPayload.BlogPost).ConfigureAwait(false);

                if (blogPost == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create failed");

                return StatusCode(StatusCodes.Status201Created, new BlogPostContainer<BlogPostPayload> { BlogPost = blogPost });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates a specific blog post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "blogPost": {
        ///             "title": "React Why and How?"
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <param name="slug"></param>
        /// <param name="requestPayload"></param>
        // PUT api/<PostsController>/:slug
        [HttpPut("{slug}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated", Type = typeof(BlogPostContainer<BlogPostPayload>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Update Failed", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult<ActionResult<BlogPostContainer<BlogPostPayload>>>> UpdateBlogPostAsync(string slug, [FromBody] BlogPostContainer<BlogPostUpdatePayload> requestPayload)
        {
            try
            {
                if (string.IsNullOrEmpty(slug) && (requestPayload == null || requestPayload.BlogPost == null))
                    return BadRequest();

                var blogPost = await _postsService.UpdateBlogPostAsync(slug, requestPayload.BlogPost).ConfigureAwait(false);

                if (blogPost == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Update failed for: {slug}");

                return Ok(new BlogPostContainer<BlogPostPayload> { BlogPost = blogPost });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific blog post
        /// </summary>
        /// <param name="slug"></param>
        // DELETE api/<PostsController>/:slug
        [HttpDelete("{slug}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Deleted")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Delete Failed", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult> DeleteBlogPostAsync(string slug)
        {
            try
            {
                if (string.IsNullOrEmpty(slug))
                    return BadRequest();

                var isDeleted = await _postsService.DeleteBlogPostAsync(slug).ConfigureAwait(false);

                if (isDeleted)
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, $"Delete failed for: {slug}");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
