using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class TagRepository(DataContext context) : ITagRepository
{
    public async Task<List<Tag>> GetAllAsync() =>
        await context.Tags.ToListAsync();

    public async Task<Tag?> GetByIdAsync(int id) =>
        await context.Tags.FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Tag tag)
    {
        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        context.Tags.Update(tag);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
    }
}