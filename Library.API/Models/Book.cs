using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.API.Models;

public class Book
{
    [Key]
    public string ISBN { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Author { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<BookTransaction> BookTransactions { get; set; }

    internal void Deconstruct(out string ISBN, out string Name, out string Author, out IEnumerable<BookTransaction> BookTransactions)
    {
        ISBN = this.ISBN;
        Name = this.Name;
        Author = this.Author;
        BookTransactions = this.BookTransactions;
    }
}
