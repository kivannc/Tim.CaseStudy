using System.Linq.Expressions;
using Library.API.Models;

namespace Library.API.Repository.Interface;

public interface ITransactionRepository
{
    Task<IEnumerable<BookTransaction>> GetAll();

    Task<IEnumerable<BookTransaction>> GetManyAsync(Expression<Func<BookTransaction, bool>> predicate);

    Task<BookTransaction> GetTransactionByIdAsync(int id);

    Task<BookTransaction> GetTransactionByISBNAsync(string isbn);
    
    void AddBookTransaction(BookTransaction transaction);

    void UpdateBookTransaction(BookTransaction transaction);

    Task<bool> SaveChangesAsync();

}