using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.CommentDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
    Task<Response<List<CommentGetDto>>> GetAllAsync(CommentFilter filter);
    Task<Response<CommentGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CommentCreateDto dto);
    Task<Response<string>> UpdateAsync(CommentUpdateDto dto);
    Task<Response<string>> DeleteAsync(int id);
}