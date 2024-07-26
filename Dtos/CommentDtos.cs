

namespace Blog.Api.Dtos;

public record CreateComment (string CommentText, DateTime CreatedDate, int PostId);

public record UpdateComment (int Id, string CommentText, int PostId);

public record CommentDto (int Id, string CommentText, int PostId, DateTime CreatedDate, DateTime ? UpdatedDate);