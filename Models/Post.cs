using System.Collections.ObjectModel;

namespace Blog.Api.Models;
public class Post
{
    public int Id { set; get; }
    public string Title { set; get; }
    public string BlogPost { set; get; }
    public DateTime CreatedDate { set; get; }
    public DateTime ? UpdatedDate { set; get; }
    public int CommentsId { set; get; }
    public int UserId { set; get; }
    public ICollection<Comments> Comments { set; get; } = [];
    public User User { set; get; }  
}

