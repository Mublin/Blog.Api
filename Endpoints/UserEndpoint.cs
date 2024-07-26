using Blog.Api.Data;
using Blog.Api.Dtos;
using Blog.Api.Models;
using Blog.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Api.Endpoints;

public static class UserEndpoint
{
    public static RouteGroupBuilder MapUserEndpoint (this WebApplication app) 
    {
        var group = app.MapGroup("/api/users");

        group.MapGet("/", async (BlogStoreContext dbContext, ClaimsPrincipal user) =>
       {
           var users = await dbContext.Users.Select(p => p.UsersToDtos()).ToListAsync();
           return users;
       }).RequireAuthorization("AdminPolicy");

        group.MapPost("/create", async (CreateUserDto newUser, BlogStoreContext dbContext, IConfiguration config) => 
        {
            User user = new()
            {
                Name = newUser.Name,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                UserName = newUser.UserName,
                CreatedDate = DateTime.Now
            };
            Hash newHash = new () { HashedPass = newUser.Hash, User = user };
            user.Hash = newHash;
            
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var Token = user.GenerateToken(config);
            return Results.Created($"/{user.Id}", user.UserToDto(Token));
        });

        group.MapPut("/{id}", async (UpdateUserDto updateUser, int id, BlogStoreContext dbContext, IConfiguration config) => {
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null) 
            {
                return Results.NotFound();
            }
            User newUser = new User()
            {
                Id = user.Id,
                Comments = user.Comments,
                CreatedDate = user.CreatedDate,
                Email = updateUser.Email,
                PhoneNumber = updateUser.PhoneNumber,
                UserName = updateUser.UserName,
                Name = updateUser.Name,
                Hash = user.Hash,
                HashId = user.HashId,
                Posts = user.Posts,
                IsAdmin = user.IsAdmin
            };
            var Token = newUser.GenerateToken(config);
            dbContext.Users.Entry(user).CurrentValues.SetValues(newUser);
            await dbContext.SaveChangesAsync();
            return Results.Ok(newUser.UserToDto(Token));
        });

        return group;
    }
}

