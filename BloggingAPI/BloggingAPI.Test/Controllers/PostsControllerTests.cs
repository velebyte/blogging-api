using BloggingAPI.ApplicationCore;
using BloggingAPI.Controllers;
using BloggingAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BloggingAPI.Test.Controllers
{
    public class PostsControllerTests
    {
        private readonly Mock<IPostsService> _postsServiceStub = new();

        [Fact]
        public async Task GetBlogPostsAsync_WithNoResult_ReturnsNoContent()
        {
            // Arrange
            List<BlogPostPayload> expectedItem = new();

            _postsServiceStub.Setup(service => service.GetBlogPostsAsync(It.IsAny<string>()))
                            .ReturnsAsync(expectedItem);

            var controller = new BlogPostsController(_postsServiceStub.Object);
            var tag = "tag1";

            // Act
            var actionResult = await controller.GetBlogPostsAsync(tag);

            // Assert
            Assert.IsType<NoContentResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetBlogPostsAsync_WithTwoBlogPosts_ReturnsTwoBlogPosts()
        {
            // Arrange
            List<BlogPostPayload> expectedItem = new();
            expectedItem.Add(CreateBlogPostPayload());
            expectedItem.Add(CreateBlogPostPayload());
            var tag = "tag1";

            _postsServiceStub.Setup(service => service.GetBlogPostsAsync(It.IsAny<string>()))
                            .ReturnsAsync(expectedItem);

            var controller = new BlogPostsController(_postsServiceStub.Object);
            

            // Act
            var actionResult = await controller.GetBlogPostsAsync(tag);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);

            var okObjectResult = actionResult.Result as OkObjectResult;
            var payloadContainer = okObjectResult.Value as BlogPostsContainer;

            Assert.NotEmpty(payloadContainer.BlogPosts);
            Assert.Equal(expectedItem.Count, payloadContainer.PostsCount);

            foreach (var blogPost in payloadContainer.BlogPosts)
                Assert.Contains(tag, blogPost.TagList);
        }

        [Fact]
        public async Task GetBlogPostBySlugAsync_WithNoSlug_ReturnsBadRequest()
        {
            // Arrange
            BlogPostPayload expectedItem = null;
            string slug = null;

            _postsServiceStub.Setup(service => service.GetBlogPostBySlugAsync(It.IsAny<string>()))
                            .ReturnsAsync(expectedItem);

            var controller = new BlogPostsController(_postsServiceStub.Object);

            // Act
            var actionResult = await controller.GetBlogPostBySlugAsync(slug);

            // Assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetBlogPostBySlugAsync_WithSlug_ReturnsNoContent()
        {
            // Arrange
            BlogPostPayload expectedItem = null;
            var slug = "slug";

            _postsServiceStub.Setup(service => service.GetBlogPostBySlugAsync(It.IsAny<string>()))
                            .ReturnsAsync(expectedItem);

            var controller = new BlogPostsController(_postsServiceStub.Object);

            // Act
            var actionResult = await controller.GetBlogPostBySlugAsync(slug);

            // Assert
            Assert.IsType<NoContentResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetBlogPostBySlugAsync_WithSlug_ReturnsBlogPost()
        {
            // Arrange
            var expectedItem = CreateBlogPostPayload();
            var slug = "slug";

            _postsServiceStub.Setup(service => service.GetBlogPostBySlugAsync(It.IsAny<string>()))
                            .ReturnsAsync(expectedItem);

            var controller = new BlogPostsController(_postsServiceStub.Object);

            // Act
            var actionResult = await controller.GetBlogPostBySlugAsync(slug);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);

            var okObjectResult = actionResult.Result as OkObjectResult;
            var payloadContainer = okObjectResult.Value as BlogPostContainer<BlogPostPayload>;
            Assert.Equal(expectedItem.Slug, payloadContainer.BlogPost.Slug);
        }



        private BlogPostPayload CreateBlogPostPayload()
        {
            return new()
            {
                Title = "Unit test title",
                Description = "Unit test description",
                Body = "Unit test body",
                Slug = "unit-test-title",
                TagList = new List<string> { "tag1", "tag2" },
                CreatedAt = DateTime.Now
            };
        }
    }
}
