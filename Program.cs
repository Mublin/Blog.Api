using Blog.Api.Data;
using Blog.Api.Endpoints;
using Blog.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.UseSecurityTokenValidators = true;
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("isAdmin", "True")
        );
});


builder.Services.AddSqlServer<BlogStoreContext>(builder.Configuration.GetConnectionString("BlogsDB"));
var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapUserEndpoint();
app.MapBlogsControllers();
app.MapCommentsEndpoints();
app.Run();
