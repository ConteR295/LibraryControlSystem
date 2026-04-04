using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LibraryControlSystem_Infrastructure;

namespace LibraryControlSystem_Persistence;

//* Factory,который создаёт dbContext
public class LibraryControlSystem_Persistence : IDesignTimeDbContextFactory<LibraryAppDbContext>
{
    public LibraryAppDbContext CreateDbContext(string[] args)
    {
        var dbPath = ConnectionStrings.ConnectionStringSqlite;
        var optionsBuilder = new DbContextOptionsBuilder<LibraryAppDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new LibraryAppDbContext(optionsBuilder.Options);
    }
}
