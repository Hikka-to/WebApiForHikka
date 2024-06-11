using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiForHikka.Domain.Models;

public class User : IdentityUser<Guid>, IModel, ICloneable
{
    [Required]
    public override required string Email { get => base.Email!; set => base.Email = value; }

    [Required]
    public required string Role { get; set; }

    public User Clone() => (User)MemberwiseClone();
    object ICloneable.Clone() => Clone();
}