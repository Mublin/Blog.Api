using Blog.Api.Dtos;
using Blog.Api.Models;

namespace Blog.Api.Services;

public static class CommentServices
{
    public static Comments CommentToModel(this CreateComment createComment) 
    {
        Comments comments = new ()
        {
            PostId = createComment.PostId,
            CommentText = createComment.CommentText,
            CreatedDate = DateTime.Now
        };
        return comments;
    }
    public static CommentDto CommentToDto(this Comments comment) 
    {
        CommentDto ToDto = new (Id: comment.Id, CommentText: comment.CommentText, PostId: comment.PostId, CreatedDate: comment.CreatedDate, UpdatedDate: comment.UpdatedDate);
        return ToDto;
    }
}
