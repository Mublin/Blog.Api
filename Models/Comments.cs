using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Models;

public class Comments
{
    public int Id { get; set; }
    public string CommentText { get; set; } = null!;
    public DateTime CreatedDate { set; get; }
    public DateTime UpdatedDate { set; get; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
}