using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DbContext
{

    public static class PrepDb
    {
        private static ILogger _logger;

        public static void PrepPopulation(IApplicationBuilder app, bool isProduction, ILogger logger)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            _logger = logger;

            SeedData(serviceScope.ServiceProvider.GetService<LibraryDbContext>(), isProduction);
        }

        private static void SeedData(LibraryDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                _logger.LogInformation("Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            try
            {
                SeedBooks(context);
                SeedMembers(context);
                SeedBookTransactions(context);
            }
            catch (Exception e)
            {
                _logger.LogError("Error On Seed Data" + e.Message);
            }

        }

        private static void SeedBookTransactions(LibraryDbContext context)
        {
            if (context.BookTransactions.Any()) return;
            
            _logger.LogInformation("Seeding Book Transactions...");

            context.BookTransactions.AddRange(
                new BookTransaction
                {
                    Id = 1,
                    ISBN = "978-9-56-148410-0",
                    MemberId = 1,
                    BorrowDate = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now.AddDays(30),
                    ReturnDate = null,

                },
                new BookTransaction
                {
                    Id = 2,
                    ISBN = "978-9-56-148410-1",
                    MemberId = 2,
                    BorrowDate = DateTime.Now.AddDays(-45),
                    DueDate = DateTime.Now.AddDays(-15),
                    ReturnDate = null
                }
            );

        }

        private static void SeedMembers(LibraryDbContext context)
        {
            if (context.Members.Any()) return;
            
            _logger.LogInformation("Seeding Members...");

            context.Members.AddRange(
                
                new Member
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@gmail.com"
                },
                new Member()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@gmail.com"
                },
                new Member()
                {
                    Id = 3,
                    FirstName = "Jack",
                    LastName = "Doe",
                    Email = "jack@gmail.com"
                }
            );
            context.SaveChanges();
        }

        private static void SeedBooks(LibraryDbContext context)
        {
            if (context.Books.Any()) return;
            
            _logger.LogInformation("Seeding Book Data");

            context.Books.AddRange(
                new Book
                {
                    ISBN = "978-9-56-148410-0",
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Author = "Douglas Adams"
                },
                new Book
                {
                    ISBN = "978-7-26-148410-1",
                    Name = "The Restaurant at the End of the Universe",
                    Author = "Rowling, J. K."
                },
                new Book
                {
                    ISBN = "978-8-16-148410-2",
                    Name = "Kingdom of Ash (Throne of Glass, 7)",
                    Author = "Maas, Sarah J."
                },
                new Book
                {
                    ISBN = "978-7-16-148410-3",
                    Name = "Queen of Shadows (Throne of Glass, 4)",
                    Author = "Maas, Sarah J."
                },
                new Book
                {
                    ISBN = "978-6-16-148410-4",
                    Name = "A Game of Thrones (A Song of Ice and Fire, Book 1)",
                    Author = "Martin, George R. R."
                },
                new Book
                {
                    ISBN = "978-5-16-148410-5",
                    Name = "A Clash of Kings (A Song of Ice and Fire, Book 2)",
                    Author = "Martin, George R. R."
                },
                new Book
                {
                    ISBN = "978-1-16-148410-6",
                    Name = "A Storm of Swords (A Song of Ice and Fire, Book 3)",
                    Author = "Martin, George R. R."
                },
                new Book
                {
                    ISBN = "978-2-13-148410-7",
                    Name = "A Feast for Crows (A Song of Ice and Fire, Book 4)",
                    Author = "Martin, George R. R."
                },
                new Book
                {
                    ISBN = "978-3-13-148410-8",
                    Name = "A Dance with Dragons (A Song of Ice and Fire, Book 5)",
                    Author = "Martin, George R. R."
                }
            );

            context.SaveChanges();
        }
    }
}

