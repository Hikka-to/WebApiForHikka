using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApiForHikka.Domain.Models;

public class User : IdentityUser<Guid>, IModel, ICloneable
{
    [Required]
    public override required string Email
    {
        get => base.Email!;
        set => base.Email = value;
    }

    public ICollection<IdentityRole<Guid>> Roles { get; set; } = [];

    object ICloneable.Clone()
    {
        return Clone();
    }

    public User Clone()
    {
        return (User)MemberwiseClone();
    }
}