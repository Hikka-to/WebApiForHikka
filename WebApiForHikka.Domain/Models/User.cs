using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApiForHikka.Domain.Models;
[Index(nameof(Email), IsUnique = true)]
public class User : Model
{
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Role { get; set; }
}