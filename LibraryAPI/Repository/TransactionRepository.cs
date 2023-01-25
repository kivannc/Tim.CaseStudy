using System.Linq.Expressions;
using LibraryAPI.DbContext;
using LibraryAPI.Models;
using LibraryAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly LibraryDbContext _context;

    public TransactionRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookTransaction>> GetManyAsync(Expression<Func<BookTransaction, bool>> predicate)
    {
        return await _context.BookTransactions.Include(t=> t.Book).Include(t=>t.Member).Where(predicate).ToListAsync();
    }

    public void AddBookTransaction(BookTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        _context.BookTransactions.Add(transaction);
    }

    public void UpdateBookTransaction(BookTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }
        _context.BookTransactions.Update(transaction);
       
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}