using LibraryControlSystem_Domain.Entities;

namespace LibraryControlSystem_Domain.Interfaces;

//* Интерейс,описывающий операции с авторами
public interface IAuthorOperationsAsync
{
    public Task AddAuthorAsync(AuthorEntity author);
    public Task<List<AuthorEntity>> GetAllAuthorsAsync();
}
