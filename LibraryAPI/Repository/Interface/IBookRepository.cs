using LibraryAPI.Models;
using System.Linq.Expressions;
using System;

namespace LibraryAPI.Repository.Interface;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> GetManyAsync(Expression<Func<Book, bool>> predicate);
    Task<IEnumerable<Book>> GetManyAsync(string search);
    Task<Book> GetBookByIdAsync(Guid id);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Book book);
    Task<bool> SaveChangesAsync();

}