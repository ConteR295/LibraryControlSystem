namespace LibraryControlSystem_Domain.Entities;

public class UserEntity //Сущность пользователя
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<BookEntity> Books{ get; set; } = new(); 
}
