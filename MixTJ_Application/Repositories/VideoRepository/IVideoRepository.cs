using Domain.Entites;

namespace MixTJ_Application.Interfaces.Repositories;

public interface IVideoRepository
{
    Task<List<Video>> GetAllAsync();
    Task<Video?> GetByIdAsync(int id);
    Task AddAsync(Video video);
    Task DeleteAsync(Video video);
}