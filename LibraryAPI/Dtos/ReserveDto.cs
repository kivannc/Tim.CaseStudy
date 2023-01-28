namespace LibraryAPI.Dtos;

public class ReserveDto
{
    public string ISBN { get; set; }
    public int MemberId { get; set; }
    public DateTime DueDate { get; set; }
}