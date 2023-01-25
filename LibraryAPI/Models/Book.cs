using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models;

public class Book
{
    [Key]
    public string ISBN { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Author { get; set; }

    public ICollection<BookTransaction> BookTransactions { get; set; }

    //public BookStatus BookStatus
    //{
    //    get
    //    {
    //        if (BookTransactions == null) return BookStatus.Available;
    //        if (BookTransactions.Count == 0)
    //        {
    //            return BookStatus.Available;
    //        }
    //        var transaction = BookTransactions.FirstOrDefault(t => t.ReturnDate == null);
    //        if (transaction == null) return BookStatus.Available;
    //        if (DateTime.Now > transaction.DueDate)
    //        {
    //            return BookStatus.Overdue;
    //        }

    //        return BookStatus.Borrowed;
    //    }
    //}
}
