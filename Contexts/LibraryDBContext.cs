using Microsoft.EntityFrameworkCore;

namespace matthew_prac;

public class LibraryDBContext : DbContext
{
    public LibraryDBContext(DbContextOptions<LibraryDBContext> options): base(options)
    {
        Database.EnsureCreated();
    }
            
     public DbSet<Accounts> Accounts { get; set; }
     public DbSet<Authors> Authors { get; set; }
}