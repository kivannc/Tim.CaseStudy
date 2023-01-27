using LibraryAPI.Models;

namespace LibraryAPI.Dtos;

public class BookDto
{
    public string ISBN { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public BookStatus BookStatus { get; set; }
}