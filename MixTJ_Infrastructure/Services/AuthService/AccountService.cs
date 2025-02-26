using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Dtos.Account;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MixTJ_Application.Dtos.AuthDto;
using MixTJ_Application.Responses;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Services;

public class AccountService( 
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : IAccountService
{
    public async Task<Response<string>> LoginAsync(LoginDto request)
    {
        var existingUser = await userManager.FindByNameAsync(request.Username);
        // if (existingUser != null) return new Response<string>(HttpStatusCode.BadRequest, $"User already exists");

        var result = await userManager.CheckPasswordAsync(existingUser, request.Password);
        if (!result) return new Response<string>(HttpStatusCode.BadRequest, $"Invalid username or password");
        
        var token = await GenerateJwtToken(existingUser);
        return new Response<string>(HttpStatusCode.OK, token);

    }

    #region GenerateJwtToken
    private async Task<string> GenerateJwtToken(User user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            expires: DateTime.UtcNow.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion

    public async Task<Response<string>> RegisterAsync(RegisterDto request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null) return new Response<string>(HttpStatusCode.BadRequest, $"User already exists");
        var newUser = new User()
        {
            UserName = request.Username,
            Email = request.Email,
            Profile = request.Profile,
        };
        var result = await userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded) return new Response<string>(HttpStatusCode.BadRequest, $"Error during registration");
        return new Response<string>(HttpStatusCode.OK, "You have successfully completed registration");
    }

    public async Task<Response<string>> AddRoleToUserAsync(RoleDto userRole)
    {
        var role = await roleManager.FindByIdAsync(userRole.RoleId);
        if (role == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Role not found");
        }
        var user = await userManager.FindByIdAsync(userRole.UserId);
        if (user == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "User not found");
        }

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        return !result.Succeeded 
            ? new Response<string>(HttpStatusCode.BadRequest, "Some thing went wrong") 
            : new Response<string>("Role successfully added from user");
    }

    public async Task<Response<string>> RemoveRoleFromUserAsync(RoleDto userRole)
    {
        var role = await roleManager.FindByIdAsync(userRole.RoleId);
        if (role == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Role not found");
        }
        var user = await userManager.FindByIdAsync(userRole.UserId);
        if (user == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "User not found");
        }

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        return !result.Succeeded 
            ? new Response<string>(HttpStatusCode.BadRequest, "Some thing went wrong") 
            : new Response<string>("Role successfully assigned to user");
    }
}