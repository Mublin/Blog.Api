using Blog.Api.Models;

namespace Blog.Api.Dtos;

public record CreatePost (string Title, string BlogPost, DateTime CreatedDate, DateTime ? UpdatedDate);

public record UpdatePost (int Id, string Title, string BlogPost, ICollection<Comments> Comments, DateTime CreatedDate, DateTime UpdatedDate);

public record PostDto (int Id, string Title, string BlogPost, ICollection<Comments> Comments, DateTime CreatedDate, DateTime ? UpdatedDate);