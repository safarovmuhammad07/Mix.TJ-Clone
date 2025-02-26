using Domain.Dtos.Account;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixTJ_Application.Dtos.AuthDto;
using MixTJ_Application.Responses;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccoutController(IAccountService service) : ControllerBase
{
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<Response<string>> Register(RegisterDto request)
    {
        return await service.RegisterAsync(request);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<Response<string>> Login(LoginDto request)
    {
        return await service.LoginAsync(request);
    }
    
    [HttpPost("add-role-to-user")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> AddRoleToUser(RoleDto request)
    {
        return await service.AddRoleToUserAsync(request);
    }
    
    [HttpDelete("remove-role-from-user")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> RemoveRoleFromUser(RoleDto request)
    {
        return await service.RemoveRoleFromUserAsync(request);
    }
}