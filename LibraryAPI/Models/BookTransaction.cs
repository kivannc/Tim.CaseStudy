using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models;

public class BookTransaction
{
    
    public int Id { get; set; }
    
    [ForeignKey(nameof(Book))]
    public string ISBN { get; set; }

    public virtual Book Book { get; set; }
    
    [ForeignKey(nameof(Member))]
    public int MemberId { get; set; }

    public virtual Member Member { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public BookStatus BookStatus
    {
        get
        {
            if (ReturnDate != null) return BookStatus.Available;
            if (DateOnly.FromDateTime(DueDate) >= DateOnly.FromDateTime(DateTime.Now)) return BookStatus.Borrowed;
            return BookStatus.Overdue;
        }
    }

}

public enum BookStatus
{
    Available,
    Borrowed,
    Overdue
}