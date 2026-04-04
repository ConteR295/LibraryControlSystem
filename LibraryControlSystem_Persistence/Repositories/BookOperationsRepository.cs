using System.Security.Cryptography.X509Certificates;
using LibraryControlSystem_Domain.Entities;
using LibraryControlSystem_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryControlSystem_Persistence;

//* Репозиторий,хранящий операции с книгами в БД
public class BookOperationsRepository : IBookOperationsAsync
{
    //! Здесь передаётся DbContext для работы с БД
    private readonly LibraryAppDbContext _dbContext;
    public BookOperationsRepository(LibraryAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddBookAsync(BookEntity book, int authorId) //Добавление новой книги
    {
        var authorEntity = _dbContext.Authors
            .FirstOrDefault(x => x.Id == authorId);

        if (authorEntity is not null)
        {
            book.Author = authorEntity;
            book.AuthorId = authorId;

            authorEntity.Books.Add(book);

            await _dbContext.Books
                .AddAsync(book);
            
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(int bookId) //Удаление книги
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
         
        if(book is not null)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<BookEntity>> GetAllBooksAsync() //Получение всех книг
    {
        return await _dbContext.Books
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<List<BookEntity>> GetBooksByAuthorAsync(int authorId) //Получение книг,которых написал определённый автор
    {
        var query = _dbContext.Books.AsNoTracking();

        query = query.Where(b => b.AuthorId == authorId);

        return await query.ToListAsync();
    }

    public async Task<List<BookEntity>> GetBooksByGenreAsync(string genre) //Получение книг одного жанра
    {
        var query = _dbContext.Books.AsNoTracking();

        query = query.Where(b => b.Genre.Equals(genre));

        return await query.ToListAsync();
    }
}
