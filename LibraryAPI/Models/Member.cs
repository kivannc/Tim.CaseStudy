using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models;

public class Member
{

    public int Id { get; set; }

    [Required] 
    public string FirstName { get; set; }

    [Required] 
    public string LastName { get; set; }

    [Required] 
    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

}