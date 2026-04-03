using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryControlSystem_Persistence;

//* Репозиторий,хранящий операции с авторами в БД
public class AuthorOperationsRepository : IAuthorOperationsAsync
{
    //! Здесь передаётся DbContext для работы с БД
    private readonly LibraryAppDbContext _dbContext;
    public AuthorOperationsRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAuthorAsync(AuthorEntity author) //Добавление нового автора
    {
        await _dbContext.Authors
            .AddAsync(author);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<AuthorEntity>> GetAllAuthorsAsync() //Получение всех авторов
    {
        return await _dbContext.Authors
                    .AsNoTracking()
                    .ToListAsync();
    }
}
