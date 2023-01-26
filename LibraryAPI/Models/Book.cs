using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Models;

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


}
