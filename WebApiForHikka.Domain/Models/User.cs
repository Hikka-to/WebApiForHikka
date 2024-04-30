using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.Domain.Models;
public class User : Model
{

    [Required]
    public string Password { get; set; }

    [Required]
    [Index(IsUnique = true)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Role { get; set; }

    public User(string password, string email, string role)
    {
        this.Password = password;
        this.Email = email;
        this.Role = role;
    }
}
