using Library.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.API.DbContext
{

    public static class PrepDb
    {

        public static async Task SeedData(LibraryDbContext context, UserManager<AppUser> userManager)
        {

            await context.Database.MigrateAsync();

            var books = await SeedBooks(context);
            var members = await SeedMembers(context);
            await SeedBookTransactions(context, books, members);
            await SeedHolidays(context);
            await SeedUsers(context, userManager);

        }

        private static async Task SeedUsers(LibraryDbContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new()
                    {
                        UserName = "Admin",
                        FirstName = "Test",
                        LastName = "User",
                        Email = "admin@gmail.com"
                    },
                    new()
                    {
                        UserName = "kivannc",
                        FirstName = "Kivanc",
                        LastName = "Erturk",
                        Email = "erturkkivanc@gmail.com"
                    }
                };

                foreach (var appUser in users)
                {
                    var result = await userManager.CreateAsync(appUser, "232421");
                }
            }

        }

        private static async Task SeedHolidays(LibraryDbContext context)
        {
            // I considered the option for calculating moving holidays for next years but
            // I could not find a reliable way to do it. Start days differs according to place on earth and placement of the moon
            // There are some API's online for this purpose. 
            // A different repository can be implemented and replaced with the current one. 
            // Now there are only holidays for 2023.

            if (context.Holidays.Any()) return;

            var holidays = new List<Holiday>
            {
                new()
                {
                    Name = "Yeni Yıl Tatili",
                    Description = "1",
                    Date = new DateTime(2023, 1, 1)
                },
                new()
                {
                    Name = "Ramazan Bayramı Arifesi",
                    Description = "0.5",
                    Date = new DateTime(2023, 4, 20)
                },
                new()
                {
                    Name = "Ramazan Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 4, 21)
                },
                new()
                {
                    Name = "Ramazan Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 4, 22)
                },
                new()
                {
                    Name = "Ramazan Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 4, 23)
                },
                new()
                {
                    Name = "Ulusal Egemenlik ve Çocuk Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 4, 23)
                },
                new()
                {
                    Name = "Emek ve Dayanışma Günü",
                    Description = "1",
                    Date = new DateTime(2023, 5, 1)
                },
                new()
                {
                    Name = "Atatürk’ü Anma Gençlik ve Spor Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 5, 19)
                },
                new()
                {
                    Name = "Kurban Bayramı Arifesi",
                    Description = "0.5",
                    Date = new DateTime(2023, 6, 27)
                },
                new()
                {
                    Name = "Kurban Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 6, 28)
                },
                new()
                {
                    Name = "Kurban Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 6, 29)
                },
                new()
                {
                    Name = "Kurban Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 6, 30)
                },
                new()
                {
                    Name = "Kurban Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 7, 1)
                },
                new()
                {
                    Name = "Demokrasi ve Milli Birlik Günü",
                    Description = "1",
                    Date = new DateTime(2023, 7, 15)
                },
                new()
                {
                    Name = "Zafer Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 8, 30)
                },
                new()
                {
                    Name = "Cumhuriyet Bayramı Arifesi",
                    Description = "0.5",
                    Date = new DateTime(2023, 10, 28)
                },
                new()
                {
                    Name = "Cumhuriyet Bayramı",
                    Description = "1",
                    Date = new DateTime(2023, 10, 29)
                }
            };

            await context.AddRangeAsync(holidays);
            await context.SaveChangesAsync();
        }

        private static async Task SeedBookTransactions(LibraryDbContext context, List<Book> books, List<Member> members)
        {

            if (books == null || members == null || context.BookTransactions.Any()) return;

            context.BookTransactions.AddRange(
                new BookTransaction
                {
                    Book = books[0],
                    Member = members[0],
                    BorrowDate = DateTime.Today.AddDays(-5),
                    DueDate = DateTime.Today.AddDays(30),
                    ReturnDate = null
                },
                new BookTransaction
                {
                    Book = books[1],
                    Member = members[1],
                    BorrowDate = DateTime.Today.AddDays(-45),
                    DueDate = DateTime.Today.AddDays(-15),
                    ReturnDate = null
                },
                new BookTransaction
                {
                    Book = books[2],
                    Member = members[2],
                    BorrowDate = DateTime.Today.AddDays(-30),
                    DueDate = DateTime.Today,
                    ReturnDate = DateTime.Today.AddDays(-25)
                },
                new BookTransaction
                {
                    Book = books[3],
                    Member = members[3],
                    BorrowDate = DateTime.Today.AddDays(-30),
                    DueDate = DateTime.Today.AddDays(0),
                    ReturnDate = null
                },
                new BookTransaction
                {
                    Book = books[4],
                    Member = members[4],
                    BorrowDate = DateTime.Today.AddDays(-28),
                    DueDate = DateTime.Today.AddDays(+1),
                    ReturnDate = null
                },
                new BookTransaction
                {
                    Book = books[5],
                    Member = members[4],
                    BorrowDate = DateTime.Today.AddDays(-15),
                    DueDate = DateTime.Today.AddDays(+2),
                    ReturnDate = null
                }
            );

            await context.SaveChangesAsync();

        }

        private static async Task<List<Member>> SeedMembers(LibraryDbContext context)
        {
            if (context.Members.Any()) return null;

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
                ,new()
                {
                    FirstName = "Jill",
                    LastName = "Doe",
                    Email = "jill@gmail.com"
                },
                new()
                {
                    FirstName = "James",
                    LastName = "Doe",
                    Email = "james@gmail.com"
                }
            };
            await context.Members.AddRangeAsync(members);
            await context.SaveChangesAsync();
            return members;
        }

        private static async Task<List<Book>> SeedBooks(LibraryDbContext context)
        {
            if (context.Books.Any()) return null;

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
                },
                new()
                {
                    ISBN = "978-0-43-635022-1",
                    Name = "1984",
                    Author = "George Orwell"
                },
                new()
                {
                    ISBN = "978-0-43-635022-2",
                    Name = "Animal Farm",
                    Author = "George Orwell"
                },
                new ()
                {
                    ISBN = "978-0-43-635022-3",
                    Name = "The Lord of the Rings",
                    Author = "J. R. R. Tolkien"
                },
                new ()
                {
                        ISBN = "978-0-43-635022-4",
                        Name = "The Hobbit",
                        Author = "J. R. R. Tolkien"
                },
            };
            
            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
            return books;
        }
    }
}

