using BloggingAPI.ApplicationCore;
using BloggingAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;
        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        // GET: api/<TagsController>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(TagsContainer))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No Content")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Exception", Type = typeof(string))]
        public async Task<ActionResult<TagsContainer>> Get()
        {
            try
            {
                var tags = await _tagsService.GetTagsAsync().ConfigureAwait(false);

                if (tags.Count > 0)
                    return Ok(new TagsContainer { Tags = tags });

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
