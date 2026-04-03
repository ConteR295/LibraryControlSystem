namespace LibraryControlSystem_Domain.Entities;

public class BookEntity //Сущность книги
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public AuthorEntity Author {get;set;} = new();                          
    public int Year { get; set; }
    public string Genre {get;set;} = string.Empty;
    public List<UserEntity> Users { get; set;} = new();
}
