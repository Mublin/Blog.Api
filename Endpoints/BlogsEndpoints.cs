using Blog.Api.Data;
using Blog.Api.Dtos;
using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Services;

public static class BlogsEndpoints
{
    public static RouteGroupBuilder MapBlogsControllers (this WebApplication app)
    {
        var group = app.MapGroup("api/blogs");


        group.MapGet("/",  async (BlogStoreContext dbContext) =>
        {
            var blogs = await dbContext.Posts.Select(p => p.PostToDto()).ToListAsync();
            return blogs;
        });

        group.MapGet("/{id}", async(int id, BlogStoreContext dbContext) =>
        {
            var blogs = await dbContext.Posts.Where(f=> f.Id == id).Select(p => new
            {
                Id = p.Id,
                Title = p.Title,
                BlogPost = p.BlogPost,
                CreatedDate = p.CreatedDate,
                Comments = p.Comments.Select(c => new
                {
                    c.CommentText,
                    c.CreatedDate,
                    c.PostId
                }).ToList()
            }).FirstOrDefaultAsync();

            if (blogs == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok(blogs);
            }
        });


        group.MapPut("/{id}", async (UpdatePost newPost, int id, BlogStoreContext dbContext) => {
            var blog = await dbContext.Posts.SingleOrDefaultAsync(b => b.Id == id);

            if (blog == null) 
            {
                return Results.NotFound();
            }
            Post post = new ()
            {
                Id = blog.Id,
                Title = newPost.Title,
                BlogPost = newPost.BlogPost,
                Comments = blog.Comments,
                UpdatedDate = DateTime.Now,
                CommentsId = blog.CommentsId,
                CreatedDate = blog.CreatedDate
            };

            dbContext.Entry(blog).CurrentValues.SetValues(post);
            await dbContext.SaveChangesAsync();
            return Results.Ok(blog.PostToDto());
        });

        group.MapPost("/create", async (CreatePost createPost, BlogStoreContext dbContext) => {
            Post post = createPost.PostToModel();
            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();


            return TypedResults.Created($"/{post.Id}", post.PostToDto());
        });

        group.MapDelete("/{id}",  (int id, BlogStoreContext dbContext) => 
        {
            dbContext.Posts.Where(b => b.Id == id).ExecuteDelete();
            return Results.NoContent();
        });


        return group;
    }
}
