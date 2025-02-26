using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.NewsDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;
public interface INewsService
{
    Task<Response<List<NewsGetDto>>> GetAllAsync(NewsFilter filter);
    Task<Response<NewsGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(NewsCreateDto dto);
    Task<Response<string>> UpdateAsync(NewsUpdateDto dto);
    Task<Response<string>> DeleteAsync(int id);
}