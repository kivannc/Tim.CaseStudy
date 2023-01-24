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
    public DateTime? BorrowDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

}

public enum BookStatus
{
    Available,
    Borrowed,
    Overdue
}