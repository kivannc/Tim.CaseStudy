using System.Linq.Expressions;
using Library.API.DbContext;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository
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
            var names = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var authors = author.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            IQueryable<Book> query = _context.Books;

            if (string.IsNullOrWhiteSpace(isbn))
            {
                query = query.Where(b => b.ISBN.Contains(isbn));
            }
            
            foreach (var n in names)
            {
                string temp = n;
                query = query.Where(b => b.Name.Contains(temp));
            }

            foreach (var a in authors)
            {
                string temp = a;
                query = query.Where(b => b.Author.Contains(temp)); 
            }

            return await query.ToListAsync();
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
