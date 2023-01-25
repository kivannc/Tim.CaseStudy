using LibraryAPI.Models;
using System.Linq.Expressions;

namespace LibraryAPI.Repository.Interface;

public interface ITransactionRepository
{

    Task<IEnumerable<BookTransaction>> GetManyAsync(Expression<Func<BookTransaction, bool>> predicate);

    void AddBookTransaction(BookTransaction transaction);

    void UpdateBookTransaction(BookTransaction transaction);

    Task<bool> SaveChangesAsync();

}