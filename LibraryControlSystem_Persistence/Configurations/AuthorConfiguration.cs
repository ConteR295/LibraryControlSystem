using LibraryControlSystem_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryControlSystem_Persistence.Comfigurations;

//* Здесь конфигурация для сущности автора
public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(a => a.Id); //автор имеет первичный ключ Id

        builder
            .HasMany(a => a.Books) //автор может иметь много книг
            .WithOne(b => b.Author); //но у книги есть только один автор 
    }
}