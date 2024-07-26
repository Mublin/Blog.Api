using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Api.Models;

public class Hash
{
    public int Id { get; set; }
    [Required]
    public string HashedPass { get; set; } = null!;
    public int UserId { get; set; }  // Add this line to specify the foreign key
    public User User { get; set; }
}