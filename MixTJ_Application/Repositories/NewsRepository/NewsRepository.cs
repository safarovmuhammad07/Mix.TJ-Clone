using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

using Domain.Entites;

public class NewsRepository(DataContext context) : INewsRepository
{
    public async Task<List<News>> GetAllAsync() =>
        await context.News.ToListAsync();

    public async Task<News?> GetByIdAsync(int id) =>
        await context.News.FirstOrDefaultAsync(n => n.Id == id);

    public async Task AddAsync(News news)
    {
        await context.News.AddAsync(news);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(News news)
    {
        context.News.Update(news);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(News news)
    {
        context.News.Remove(news);
        await context.SaveChangesAsync();
    }
}