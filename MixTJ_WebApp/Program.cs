using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Seed;
using MixTJ_Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);
builder.Services.SwaggerConfigurationServices();
builder.Services.AuthConfigureServices(builder.Configuration); // Убедись, что внутри уже есть AddIdentity()

// Удаляем дублирующийся вызов AddIdentity(), если он есть внутри AuthConfigureServices
/*
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
*/

var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    
    // Автоматическое применение миграций
    var datacontext = serviceProvider.GetRequiredService<DataContext>();
    await datacontext.Database.MigrateAsync();

    // Заполняем базу ролями и пользователями
    var seeder = serviceProvider.GetRequiredService<Seeder>();
    await seeder.SeedRole();
    await seeder.SeedUser();
}
catch (Exception e)
{
    Console.WriteLine($"Error during migration: {e.Message}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication(); // Используем аутентификацию перед авторизацией!
app.UseAuthorization();
app.MapControllers();
app.Run();