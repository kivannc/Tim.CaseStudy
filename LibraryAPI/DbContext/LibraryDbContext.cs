using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.DbContext;

public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext {

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BookTransaction> BookTransactions { get; set; }
}