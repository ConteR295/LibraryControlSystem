using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryControlSystem_Persistence;

//* Репозиторий,хранящий операции с пользователями в БД
public class UserOperationsRepository : IUserOperationsAsync
{
    //! Здесь передаётся DbContext для работы с БД
    private readonly LibraryAppDbContext _dbContext;
    public UserOperationsRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(UserEntity user) //Добавление нового пользователя
    {
        await _dbContext.Users
            .AddAsync(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserEntity>> GetAllUsersAsync() //Получение всех пользователей
    {
        return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();

    }

    public async Task LinkAsync(int userId, int bookId) //Соеденения пользователя и книги по их Id
    {
        var userEntity = _dbContext.Users
                        .First(u => u.Id == userId);
        var bookEntity = _dbContext.Books
                        .First(b => b.Id == bookId);
        
        userEntity.Books.Add(bookEntity);
        bookEntity.Users.Add(userEntity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserEntity updateUser) //Обновление пользователя
    {
        await _dbContext.Users
            .Where(u => u.Id == updateUser.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.FirstName, updateUser.FirstName)
            .SetProperty(u => u.LastName, updateUser.LastName)
            .SetProperty(u => u.Email, updateUser.Email)
            .SetProperty(u => u.PhoneNumber,updateUser.PhoneNumber)
            );

        await _dbContext.SaveChangesAsync();
    }
}
