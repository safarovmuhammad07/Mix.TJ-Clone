using Domain.Entites;

namespace MixTJ_Application.Interfaces.Repositories;

public interface INewsRepository
{
    Task<List<News>> GetAllAsync();
    Task<News?> GetByIdAsync(int id);
    Task AddAsync(News news);
    Task UpdateAsync(News news);
    Task DeleteAsync(News news);
}