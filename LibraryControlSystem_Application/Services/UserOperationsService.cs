using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;

namespace LibraryControlSystem_Application;

//* Сервис,выполняющий операции с пользователями
public class UserOperationsService
{
    private readonly IUserOperationsAsync _IUserOperations;

    public UserOperationsService(IUserOperationsAsync IUserOperations)
    {
        _IUserOperations = IUserOperations;
    }

    public async Task AddUser(string firstName, string lastName, string email, string phoneNumber)
    {
        var newUser = new UserEntity
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        await _IUserOperations.AddUserAsync(newUser);
    }

    public async Task UpdateUser(int Id,string firstName, string lastName, string email,string phoneNumber)
    {
        if(Id < 0) throw new ArgumentOutOfRangeException(nameof(Id));
        var updateUser = new UserEntity
        {
            Id = Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        await _IUserOperations.UpdateUserAsync(updateUser);
    }

    public async Task Link(int userId,int bookId)
    {
        if(userId < 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if(bookId < 0) throw new ArgumentOutOfRangeException(nameof(bookId));

        await _IUserOperations.LinkAsync(userId,bookId);
    }

    public async Task<List<UserEntity>> GetAllUsers()
    {
        return await _IUserOperations.GetAllUsersAsync();
    }
}
