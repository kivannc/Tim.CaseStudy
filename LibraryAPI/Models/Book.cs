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
}
