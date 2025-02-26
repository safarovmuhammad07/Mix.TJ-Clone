using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.VideoDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;
public interface IVideoService
{
    Task<Response<List<VideoGetDto>>> GetAllAsync(VideoFilter filter);
    Task<Response<VideoGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(VideoCreateDto dto);
    Task<Response<string>> DeleteAsync(int id);
}