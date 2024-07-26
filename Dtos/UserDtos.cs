using Blog.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Dtos;

public record UserDtos
    (
    int Id,
    string Name,
    string? Email,
    long? PhoneNumber,
    DateTime CreatedAt,
    string UserName,
    ICollection<Post> Posts,
    ICollection<Comments> Comments,
    string Token,
    bool isAdmin
    );

public record UsersDtos
    (
    int Id,
    string Name,
    string? Email,
    long? PhoneNumber,
    DateTime CreatedAt,
    string UserName,
    ICollection<Post> Posts,
    ICollection<Comments> Comments,
    bool isAdmin
    );

public record CreateUserDto(
    string Name,
    string? Email,
    long? PhoneNumber,
    string UserName,
    string Hash
    );

public record UpdateUserDto(
    string Name,
    string? Email,
    long? PhoneNumber,
    string UserName
    );

