using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Dtos.UserDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<Response<List<UserGetDto>>> GetAllAsync(UserFilter filter)
    {
        var users = await repository.GetAllAsync();

        if (!string.IsNullOrEmpty(filter.Email))
            users = users.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower())).ToList();
        if (!string.IsNullOrEmpty(filter.Nickname))
            users = users.Where(u => u.UserName.ToLower().Contains(filter.Nickname.ToLower())).ToList();
        if (!string.IsNullOrEmpty(filter.Profile))
            users = users.Where(u => !string.IsNullOrEmpty(u.Profile) &&
                                     u.Profile.ToLower().Contains(filter.Profile.ToLower())).ToList();

        var dtoList = mapper.Map<List<UserGetDto>>(users);
        return new Response<List<UserGetDto>>(dtoList);
    }

    public async Task<Response<UserGetDto>> GetByIdAsync(string id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user == null)
            return new Response<UserGetDto>(HttpStatusCode.NotFound, "User not found");

        var dto = mapper.Map<UserGetDto>(user);
        return new Response<UserGetDto>(dto);
    }

    public async Task<Response<string>> CreateAsync(UserCreateDto dto)
    {
        var user = mapper.Map<User>(dto);
        await repository.AddAsync(user);
        return new Response<string>(HttpStatusCode.Created, "User created");
    }

    public async Task<Response<string>> UpdateAsync(UserUpdateDto dto)
    {
        var user = await repository.GetByIdAsync(dto.Id);
        if (user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User not found");

        mapper.Map(dto, user);
        await repository.UpdateAsync(user);
        return new Response<string>(HttpStatusCode.OK, "User updated");
    }

    public async Task<Response<string>> DeleteAsync(string id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User not found");

        await repository.DeleteAsync(user);
        return new Response<string>(HttpStatusCode.OK, "User deleted");
    }
}