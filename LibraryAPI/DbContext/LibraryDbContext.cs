using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibraryAPI.DbContext;

public class LibraryDbContext : IdentityDbContext<AppUser> {

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BookTransaction> BookTransactions { get; set; }
    public DbSet<Holiday> Holidays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookTransaction>()
            .HasOne(b => b.Book)
            .WithMany(t => t.BookTransactions)
            .HasForeignKey(fk => fk.ISBN);

        base.OnModelCreating(modelBuilder);
    }
}