using System.Linq.Expressions;
using Library.API.DbContext;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly LibraryDbContext _context;

    public TransactionRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookTransaction>> GetAll()
    {
        return await _context.BookTransactions
            .Include(x => x.Book)
            .Include(x => x.Member)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookTransaction>> GetManyAsync(Expression<Func<BookTransaction, bool>> predicate)
    {
        return await _context.BookTransactions
            .Include(t => t.Book)
            .Include(t => t.Member)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<BookTransaction> GetTransactionByIdAsync(int id)
    {
        return await _context.BookTransactions
            .Include(x => x.Book)
            .Include(x => x.Member)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<BookTransaction> GetTransactionByISBNAsync(string isbn)
    {
        return await _context.BookTransactions
            .Include(x => x.Book)
            .Include(x => x.Member)
            .FirstOrDefaultAsync(t => t.ISBN == isbn && t.ReturnDate == null);
    }

    public void AddBookTransaction(BookTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        _context.BookTransactions.Add(transaction);
    }

    public void UpdateBookTransaction(BookTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        _context.BookTransactions.Update(transaction);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}