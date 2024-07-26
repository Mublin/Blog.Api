using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Api.Dtos;
using Blog.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api.Services;

public static class UserServices
{
    public static UserDtos UserToDto(this User user, string Token)
    {
        UserDtos updatedUser = new(
            Id: user.Id,
            Comments: user.Comments,
            CreatedAt: user.CreatedDate,
            Email: user.Email,
            Name: user.Name,
            PhoneNumber: user.PhoneNumber,
            Posts: user.Posts,
            UserName: user.UserName,
            Token: Token,
            isAdmin: user.IsAdmin
        );
        return updatedUser;
    }
    public static UsersDtos UsersToDtos(this User user)
    {
        UsersDtos updatedUser = new(
            Id: user.Id,
            Comments: user.Comments,
            CreatedAt: user.CreatedDate,
            Email: user.Email,
            Name: user.Name,
            PhoneNumber: user.PhoneNumber,
            Posts: user.Posts,
            UserName: user.UserName,
            isAdmin: user.IsAdmin
        );
        return updatedUser;
    }

    public static User DtoToUser(this CreateUserDto newUser)
    {
        User user = new()
        {
            Name = newUser.Name,
            Email = newUser.Email,
            PhoneNumber = newUser.PhoneNumber,
            UserName = newUser.UserName,
            CreatedDate = DateTime.Now,
        };
        return user;
    }

    public static string GenerateToken(this User user, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("id", user.Id.ToString()),
        new Claim("name", user.Name),
        new Claim("isAdmin", user.IsAdmin.ToString()),
        new Claim("username", user.UserName),
    };

    var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(24),
        signingCredentials: creds);
    return tokenHandler.WriteToken(token);
    }
}
