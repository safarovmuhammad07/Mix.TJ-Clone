using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.TagDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;
public interface ITagService
{
    Task<Response<List<TagGetDto>>> GetAllAsync(TagFilter filter);
    Task<Response<TagGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(TagCreateDto dto);
    Task<Response<string>> UpdateAsync(TagUpdateDto dto);
    Task<Response<string>> DeleteAsync(int id);
}