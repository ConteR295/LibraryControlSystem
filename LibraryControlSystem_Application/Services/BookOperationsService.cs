using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;

namespace LibraryControlSystem_Application;

//* Сервис,выполняющий операции с книгами
public class BookOperationsService
{
    private readonly IBookOperationsAsync _IBookOperations;

    public BookOperationsService(IBookOperationsAsync IBookOperations)
    {
        _IBookOperations = IBookOperations;
    }

    public async Task AddBook(BookEntity book,int authorId)
    {
        if(book == null) throw new ArgumentNullException(nameof(book));
        if(authorId < 0) throw new ArgumentOutOfRangeException(nameof(authorId));

        await _IBookOperations.AddBookAsync(book,authorId);
    }

    public async Task<List<BookEntity>> GetAllBooks()
    {
        return await _IBookOperations.GetAllBooksAsync();
    }

    public async Task<List<BookEntity>> GetBooksByGenre(string genre)
    {
        return await _IBookOperations.GetBooksByGenreAsync(genre??throw 
                                            new ArgumentNullException(nameof(genre)));
    }

    public async Task<List<BookEntity>> GetBooksByAuthor(int authorId)
    {
        return await _IBookOperations.GetBooksByAuthorAsync(authorId < 0 ?
                                                throw new ArgumentOutOfRangeException(nameof(authorId))
                                                :authorId);
    }

    public async Task DeleteBook(int id)
    {
        await _IBookOperations.DeleteBookAsync(id< 0 ?
                                                throw new ArgumentOutOfRangeException(nameof(id))
                                                :id);
    }
}
