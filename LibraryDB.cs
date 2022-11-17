using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using matthew_prac;
using Microsoft.EntityFrameworkCore;

public class LibraryDB : DbContext
{
    public LibraryDB(DbContextOptions<LibraryDB> options) : base(options)
    {
        Database.EnsureCreated(); 
    }

/*     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=LibraryDb;MultipleActiveResultSets=true;User=sa;Password=P@ssword1");
    } */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    //entities
    public DbSet<Accounts> User { get; set; }
} 