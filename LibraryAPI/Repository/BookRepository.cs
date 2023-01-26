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

        public async Task<IEnumerable<Book>> GetManyAsync(string searchString)
        {
            // This could be improved. With single dn request with sql query. 
            // Paging could be used if there too much data.
            //TODO check if you could improve this later. 
            var searchList = searchString.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
            var books = new List<Book>();
            foreach (var search in searchList)
            {
                books.AddRange(await GetManyAsync(b =>
                    b.ISBN.Contains(search) ||
                    b.Author.Contains(search) || 
                    b.Name.Contains(search))
                );
            }
            return books.Distinct();
        }

        public async Task<Book> GetBookByIdAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
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
