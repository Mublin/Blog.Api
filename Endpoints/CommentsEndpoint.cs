using Blog.Api.Data;
using Blog.Api.Dtos;
using Blog.Api.Models;
using Blog.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Endpoints;

public static class CommentsEndpoint
{
    public static RouteGroupBuilder MapCommentsEndpoints( this WebApplication app) 
    {
        var group = app.MapGroup("api/comment");


        group.MapPost("/create", async (CreateComment newComment, BlogStoreContext dbContext) => 
        {
            Comments comment = newComment.CommentToModel();
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return TypedResults.Created($"/{comment.Id}", comment.CommentToDto());
        });

        group.MapPut("/{id}", async (int id, UpdateComment updateComment, BlogStoreContext dbContext) =>
        {
            var comment = await dbContext.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return Results.NotFound();
            }

            Comments newComment = new Comments()
            {
                Id = comment.Id,
                CommentText = updateComment.CommentText,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = DateTime.Now,
                PostId = comment.PostId,
            };
            dbContext.Comments.Entry(comment).CurrentValues.SetValues(newComment);
            await dbContext.SaveChangesAsync();
            return Results.Ok(newComment.CommentToDto());
        });
        group.MapDelete("/{id}", (int id, BlogStoreContext dbContext) =>
        {
            dbContext.Comments.Where(c => c.Id == id).ExecuteDelete();
            return Results.NoContent();
        });

        return group;
    }
}
