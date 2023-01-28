namespace LibraryAPI.Dtos;

public class DueDateDto
{
    public DateTime DueDate { get; set; }
    public IEnumerable<DateOnly> Holidays { get; set; }
}
