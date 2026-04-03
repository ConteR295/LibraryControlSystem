using LibraryControlSystem_Domain.Entities;

namespace LibraryControlSystem_Domain.Interfaces;

// * Интерфейс,описывающий операции с книгами
public interface IBookOperationsAsync
{
    public Task AddBookAsync(BookEntity book,int authorId);
    public Task<List<BookEntity>> GetBooksByGenreAsync(string genre);
    public Task<List<BookEntity>> GetBooksByAuthorAsync(int authorId);
    public Task<List<BookEntity>> GetAllBooksAsync();
    public Task DeleteBookAsync(int Id);

}
