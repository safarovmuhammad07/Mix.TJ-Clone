using Domain.Dtos.Account;
using MixTJ_Application.Dtos.AuthDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;

public interface IAccountService
{
    Task<Response<string>> LoginAsync(LoginDto request);
    Task<Response<string>> RegisterAsync(RegisterDto request);
    Task<Response<string>> AddRoleToUserAsync(RoleDto userRole);
    Task<Response<string>> RemoveRoleFromUserAsync(RoleDto userRole);
}