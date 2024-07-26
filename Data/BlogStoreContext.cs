using Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Blog.Api.Data;

public class BlogStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Hash> Hashes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comments> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User - Hash one-to-one relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Hash)
            .WithOne(h => h.User)
            .HasForeignKey<Hash>(h => h.UserId);

        modelBuilder.Entity<Hash>().ToTable("Hashes");

        // Post - User one-to-many relationship
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Comment - User one-to-many relationship
        modelBuilder.Entity<Comments>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Avoid cascade delete

        // Comment - Post one-to-many relationship
        modelBuilder.Entity<Comments>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Other configurations can go here if necessary
    }
}


