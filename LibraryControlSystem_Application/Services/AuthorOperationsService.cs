using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;

namespace LibraryControlSystem_Application.Services;

//* Сервис,выполняющий операции с авторами
public class AuthorOperationsService
{
    private readonly IAuthorOperationsAsync _IAuthorOperations;

    public AuthorOperationsService(IAuthorOperationsAsync IauthorOperations)
    {
        _IAuthorOperations = IauthorOperations;
    }

    public async Task AddAuthor(string firstName,string lastName)
    {
        var newAuth = new AuthorEntity { FirstName = firstName,
                                         LastName = lastName};

        await _IAuthorOperations.AddAuthorAsync(newAuth);
    }

    public async Task GetAllAuthors()
    {
        await _IAuthorOperations.GetAllAuthorsAsync();
    }
}
