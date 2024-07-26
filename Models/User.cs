using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Email { get; set; }
    public long? PhoneNumber { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public bool IsAdmin { get; set; } = true;

    public int HashId { get; set; }
    public Hash Hash { get; set; }
    public ICollection<Post> Posts { get; set; } = [];
    public ICollection<Comments> Comments { get; set; } = [];
}
