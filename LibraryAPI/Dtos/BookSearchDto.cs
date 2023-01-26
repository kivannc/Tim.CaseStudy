using LibraryAPI.Models;

namespace LibraryAPI.Dtos;

public class BookSearchDto
{
    public string ISBN { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }
}