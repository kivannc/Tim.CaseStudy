using System.Linq.Expressions;
using Library.API.Models;

namespace Library.API.Repository.Interface;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> GetManyAsync(Book book);
    Task<IEnumerable<Book>> GetManyAsync(Expression<Func<Book, bool>> predicate);
    Task<Book> GetBookByIdAsync(string isbn);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Book book);
    Task<bool> SaveChangesAsync();

}