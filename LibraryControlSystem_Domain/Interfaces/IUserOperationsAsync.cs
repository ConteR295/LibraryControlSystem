using LibraryControlSystem_Domain.Entities;

namespace LibraryControlSystem_Domain.Interfaces;

//* Интерейса,описывающий операции с пользователями
public interface IUserOperationsAsync
{
    public Task AddUserAsync(UserEntity user);
    public Task UpdateUserAsync(UserEntity updateUser);
    public Task LinkAsync(int userId,int bookId);
    public Task<List<UserEntity>> GetAllUsersAsync();
}
