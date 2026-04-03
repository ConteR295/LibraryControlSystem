using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryControlSystem_Persistence;
using LibraryControlSystem_Application.Services;
using LibraryControlSystem_Application;
using LibraryControlSystem_Domain.Interfaces;
using LibraryControlSystem_Infrastructure;

namespace LibraryControlSystem_API;

//* Здесь подключаются зависимости
class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        // Регистрируем DbContext
        var dbPath = ConnectionStrings.ConnectionStringSqlite;
        services.AddDbContext<LibraryAppDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));

        //регистрируем репозитории
        services.AddScoped<IAuthorOperationsAsync,AuthorOperationsRepository>();
        services.AddScoped<IBookOperationsAsync,BookOperationsRepository>();
        services.AddScoped<IUserOperationsAsync,UserOperationsRepository>();

        //регистрируем сервисы
        services.AddScoped<AuthorOperationsService>();
        services.AddScoped<BookOperationsService>();
        services.AddScoped<UserOperationsService>();
        
        //создаём провайдер
        using var serviceProvider = services.BuildServiceProvider();

        var serv = serviceProvider.GetService<UserOperationsService>();

        //TODO:Осталось только сделать пользовательский ввод)
    }
}
