namespace LibraryControlSystem_Domain.Entities;

public class AuthorEntity //Сущность автора
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<BookEntity> Books { get; set; } = new();
}
