using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class VideoRepository(DataContext context) : IVideoRepository
{
    public async Task<List<Video>> GetAllAsync() =>
        await context.Videos.ToListAsync();

    public async Task<Video?> GetByIdAsync(int id) =>
        await context.Videos.FirstOrDefaultAsync(v => v.Id == id);

    public async Task AddAsync(Video video)
    {
        await context.Videos.AddAsync(video);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Video video)
    {
        context.Videos.Remove(video);
        await context.SaveChangesAsync();
    }
}