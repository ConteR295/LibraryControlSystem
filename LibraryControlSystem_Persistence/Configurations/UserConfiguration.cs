using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryControlSystem_Domain.Entities;

namespace LibraryControlSystem_Persistence.Comfigurations;

//* Здесь конфигурации для сущности пользователя
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(b => b.Id); //пользователь имеет первичный ключ Id

        builder
            .HasMany(u => u.Books) //пользователь может иметь много книг
            .WithMany(b => b.Users); // и книгу могут читать много пользователей
            
    }
}
