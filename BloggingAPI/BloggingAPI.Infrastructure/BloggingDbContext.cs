using BloggingAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BloggingAPI.Infrastructure
{
    public class BloggingDbContext : DbContext
    {
        public BloggingDbContext(DbContextOptions<BloggingDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BlogPost>(entity =>
                {
                    entity
                        .HasIndex(column => column.Title)
                        .IsUnique();

                    entity
                        .HasIndex(column => column.Slug)
                        .IsUnique();

                    entity.HasData(
                        new BlogPost
                        {
                            Id = 1,
                            Title = "What is Lorem Ipsum?",
                            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                            Body = "When an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            CreatedAt = DateTime.Now,
                            Slug = "what-is-lorem-ipsum",
                        },
                        new BlogPost
                        {
                            Id = 2,
                            Title = "Why do we use it?",
                            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.",
                            Body = "The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                            CreatedAt = DateTime.Now,
                            Slug = "why-do-we-use-it"
                        }
                    );
                });

            modelBuilder
                .Entity<Tag>(entity =>
                {
                    entity
                        .HasIndex(column => column.Name)
                        .IsUnique();

                    entity
                        .HasData(
                            new Tag { Id = 1, Name = "lorem" },
                            new Tag { Id = 2, Name = "ipsum" }
                        );
                });

            modelBuilder
                .Entity<BlogPost>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.BlogPosts)
                .UsingEntity(j => j.HasData(
                    new
                    {
                        BlogPostsId = 1,
                        TagsId = 1
                    },
                    new
                    {
                        BlogPostsId = 1,
                        TagsId = 2
                    },
                    new
                    {
                        BlogPostsId = 2,
                        TagsId = 2
                    }
                ));
        }
    }
}
