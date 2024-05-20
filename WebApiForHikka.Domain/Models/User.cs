using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;
public class User : Model
{
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [Index(IsUnique = true)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Role { get; set; }
}