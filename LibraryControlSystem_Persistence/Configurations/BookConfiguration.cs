using Microsoft.EntityFrameworkCore;
using LibraryControlSystem_Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryControlSystem_Persistence.Comfigurations;

//* Здесь конфигурация для сущности книги
public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.HasKey(b => b.Id); //книга имеет первичный ключ Id

        builder
            .HasOne(b => b.Author) //у книги может быть только один автор
            .WithMany(a => a.Books) //но автор может иметь много книг
            .HasForeignKey(b => b.AuthorId); //и у книги есть foreign key,указывающий на id автора

        builder
            .HasMany(b => b.Users) // книгу могут читать много пользователей
            .WithMany(u => u.Books); // и у пользователя может быть много книг
    }
}
