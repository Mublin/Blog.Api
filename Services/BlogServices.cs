using Blog.Api.Dtos;
using Blog.Api.Models;

namespace Blog.Api.Services;

public static class BlogServices
{
    public static Post PostToModel(this CreatePost createPost) 
    {
        Post post = new ()
        {
            Title = createPost.Title,
            BlogPost = createPost.BlogPost,
            CreatedDate = DateTime.Now,
        };
        return post;
    }
    public static PostDto PostToDto(this Post post) 
    {
        PostDto ToDto = new PostDto(Id: post.Id, Title: post.Title, BlogPost: post.BlogPost, Comments: post.Comments, CreatedDate: post.CreatedDate, UpdatedDate: post.UpdatedDate);
        return ToDto;
    }
}
