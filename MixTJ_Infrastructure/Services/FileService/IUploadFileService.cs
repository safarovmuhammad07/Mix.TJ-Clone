using Domain.Dtos;
using Domain.Filters;
using MixTJ_Application.Dtos.FileDto;
using MixTJ_Application.Responses;

namespace Infrastructure.Interfaces;

public interface IUploadFileService
{
    Task<Response<List<UploadedFileGetDto>>> GetAllAsync(UploadedFileFilter filter);
    Task<Response<UploadedFileGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(UploadedFileCreateDto dto);
    Task<Response<string>> DeleteAsync(int id);
}