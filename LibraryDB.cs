using matthew_prac.Controllers;

public class LibraryDB : DbContext
{
    public LibraryDB()
    {
  
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    //entities
    public DbSet<Accounts> Students { get; set; }
} 