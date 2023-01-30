namespace Library.API.Models;

public class Holiday
{
    public int  Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } // Change to Number of days 

    // Half day holidays assumed as library closed.
}