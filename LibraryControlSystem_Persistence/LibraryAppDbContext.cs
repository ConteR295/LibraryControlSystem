using LibraryControlSystem_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryControlSystem_Persistence.Comfigurations;

namespace LibraryControlSystem_Persistence;

//* Здесь контекст БД
public class LibraryAppDbContext : DbContext
{
    public DbSet<AuthorEntity> Authors {get;set;} //множество авторов
    public DbSet<BookEntity> Books{get;set;} //множество книг
    public DbSet<UserEntity> Users {get;set;} //множество пользователей

    public LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options)
        : base(options)
    {
        
    }

    //* Здесь подключаю конфигурации при создании моделей в БД
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration()); //Подключаю конфигурацию автора
        modelBuilder.ApplyConfiguration(new BookConfiguration()); //Подключаю конфигурацию книги
        modelBuilder.ApplyConfiguration(new UserConfiguration()); //Подключаю конфигурацию пользователя
    }
}
