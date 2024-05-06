using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
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

    public User() { }

    public User ShallowCopy() 
    {
        return new User()
        {
            Email = this.Email,
            Role = this.Role,
            Password = this.Password,
            Id = this.Id
        };
    }
}
