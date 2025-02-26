using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class UploadFileRepository(DataContext context) : IUploadFileRepository
{
    public async Task<List<UploadedFile>> GetAllAsync() =>
        await context.Files.ToListAsync();

    public async Task<UploadedFile?> GetByIdAsync(int id) =>
        await context.Files.FirstOrDefaultAsync(f => f.Id == id);

    public async Task AddAsync(UploadedFile file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UploadedFile file)
    {
        context.Files.Remove(file);
        await context.SaveChangesAsync();
    }
}