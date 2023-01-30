using Library.API.Models;

namespace Library.API.Dtos;

public class TransactionDto
{
    public int Id { get; set; }
    
    public BookDto Book { get; set; }

    public MemberDto Member { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime BorrowDate { get; set; }

    public int LateDays =>DateTime.Today > DueDate ?  (int)(DateTime.Today - DueDate).TotalDays : 0;

    public double Penalty { get; set; }

    public BookStatus BookStatus { get; set; }
}