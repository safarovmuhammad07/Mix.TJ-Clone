using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Interfaces.Repositories;

namespace Infrastructure.Repositories.Infrastructure.Repositories;

public class CommentRepository(DataContext context) : ICommentRepository
{
    public async Task<List<Comment>> GetAllAsync() =>
        await context.Comments.ToListAsync();
        
    public async Task<Comment?> GetByIdAsync(int id) =>
        await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        
    public async Task AddAsync(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
    }
        
    public async Task UpdateAsync(Comment comment)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }
        
    public async Task DeleteAsync(Comment comment)
    {
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }
}