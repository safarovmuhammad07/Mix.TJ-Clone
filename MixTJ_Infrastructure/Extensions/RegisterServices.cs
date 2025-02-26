using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Profiles;
using MixTJ_Application.Seed;

namespace MixTJ_Infrastructure.Extensions;

public static class RegisterServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUploadFileRepository, UploadFileRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IAccountService, AccountService>();

        
        services.AddScoped<Seeder>();
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddAutoMapper(typeof(EntityProfile));
    }
}