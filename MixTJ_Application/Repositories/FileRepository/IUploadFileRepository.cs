using Domain.Entites;

namespace MixTJ_Application.Interfaces.Repositories;

public interface IUploadFileRepository
{
    Task<List<UploadedFile>> GetAllAsync();
    Task<UploadedFile?> GetByIdAsync(int id);
    Task AddAsync(UploadedFile file);
    Task DeleteAsync(UploadedFile file);
}