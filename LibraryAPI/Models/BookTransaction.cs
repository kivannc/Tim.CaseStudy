using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models;

public class BookTransaction
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(Book))]
    public string ISBN { get; set; }

    [ForeignKey(nameof(Member))]
    public Guid MemberId { get; set; }
    
    public DateOnly BorrowDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }

}

public enum BookStatus
{
    Available,
    Borrowed,
    Overdue
}