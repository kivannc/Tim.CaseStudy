using System.Linq.Expressions;
using LibraryAPI.DbContext;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LibraryAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.BookTransactions.Where(bt => bt.ReturnDate == null)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetManyAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Books.Include(b => b.BookTransactions.Where(bt => bt.ReturnDate == null)).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetManyAsync(Book book)
        {
            var (isbn, name, author, _) = book;
            var books = await GetManyAsync(b =>
                (string.IsNullOrWhiteSpace(isbn) || b.ISBN.Contains(isbn)) &&
                (string.IsNullOrWhiteSpace(name) || b.Name.Contains(name)) &&
                (string.IsNullOrWhiteSpace(author) || b.Author.Contains(author)));

            return books;
        }

        public async Task<Book> GetBookByIdAsync(string isbn)
        {
            return await _context.Books.Include(b => b.BookTransactions.Where(bt => bt.ReturnDate == null)).FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public Task AddBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
