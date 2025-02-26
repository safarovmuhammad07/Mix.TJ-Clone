using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.UserDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<UserGetDto>>> GetAllAsync(UserFilter filter);
    Task<Response<UserGetDto>> GetByIdAsync(string id);
    Task<Response<string>> CreateAsync(UserCreateDto dto);
    Task<Response<string>> UpdateAsync(UserUpdateDto dto);
    Task<Response<string>> DeleteAsync(string id);
}