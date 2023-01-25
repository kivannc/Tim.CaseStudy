﻿using LibraryAPI.Models;
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

                var books = SeedBooks(context);
                var members = SeedMembers(context);
                SeedBookTransactions(context, books, members);

            }
            catch (Exception e)
            {
                _logger.LogError("Error On Seed Data" + e.Message);
            }

        }

        private static void SeedBookTransactions(LibraryDbContext context, List<Book> books, List<Member> members)
        {
            _logger.LogInformation("Seeding Book Transactions...");

            if (books == null || members == null || context.BookTransactions.Any()) return;

            context.BookTransactions.AddRange(
                new BookTransaction
                {
                    Book = books[0],
                    Member = members[0],
                    BorrowDate = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now.AddDays(30),
                    ReturnDate = null,

                }
                ,
                new BookTransaction
                {
                    Book = books[1],
                    Member = members[1],
                    BorrowDate = DateTime.Now.AddDays(-45),
                    DueDate = DateTime.Now.AddDays(-15),
                    ReturnDate = null
                }
            );

            context.SaveChanges();

        }

        private static List<Member> SeedMembers(LibraryDbContext context)
        {
            if (context.Members.Any()) return null;

            _logger.LogInformation("Seeding Members...");
            var members = new List<Member>()
            {
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@gmail.com"
                },
                new()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@gmail.com"
                },
                new()
                {
                    FirstName = "Jack",
                    LastName = "Doe",
                    Email = "jack@gmail.com"
                }
            };
            context.Members.AddRange(members);
            context.SaveChanges();
            return members;
        }

        private static List<Book> SeedBooks(LibraryDbContext context)
        {
            if (context.Books.Any()) return null;

            _logger.LogInformation("Seeding Book Data");
            var books = new List<Book>
            {
                new()
                {
                    ISBN = "978-9-56-148410-0",
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Author = "Douglas Adams"
                },
                new()
                {
                    ISBN = "978-7-26-148410-1",
                    Name = "The Restaurant at the End of the Universe",
                    Author = "Rowling, J. K."
                },
                new()
                {
                    ISBN = "978-8-16-148410-2",
                    Name = "Kingdom of Ash (Throne of Glass, 7)",
                    Author = "Maas, Sarah J."
                },
                new()
                {
                    ISBN = "978-7-16-148410-3",
                    Name = "Queen of Shadows (Throne of Glass, 4)",
                    Author = "Maas, Sarah J."
                },
                new()
                {
                    ISBN = "978-6-16-148410-4",
                    Name = "A Game of Thrones (A Song of Ice and Fire, Book 1)",
                    Author = "Martin, George R. R."
                },
                new()
                {
                    ISBN = "978-5-16-148410-5",
                    Name = "A Clash of Kings (A Song of Ice and Fire, Book 2)",
                    Author = "Martin, George R. R."
                },
                new()
                {
                    ISBN = "978-1-16-148410-6",
                    Name = "A Storm of Swords (A Song of Ice and Fire, Book 3)",
                    Author = "Martin, George R. R."
                },
                new()
                {
                    ISBN = "978-2-13-148410-7",
                    Name = "A Feast for Crows (A Song of Ice and Fire, Book 4)",
                    Author = "Martin, George R. R."
                },
                new()
                {
                    ISBN = "978-3-13-148410-8",
                    Name = "A Dance with Dragons (A Song of Ice and Fire, Book 5)",
                    Author = "Martin, George R. R."
                }
            };
            context.Books.AddRange(books);
            context.SaveChanges();
            return books;
        }
    }
}

