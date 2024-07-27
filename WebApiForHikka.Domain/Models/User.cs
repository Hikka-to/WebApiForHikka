using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models;

public class User : IdentityUser<Guid>, IModel, ICloneable
{
    [Required]
    public override required string Email
    {
        get => base.Email!;
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Email = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }

    public virtual required UserSetting UserSetting { get; set; }

    [StringLength(UserNumberConstants.NameLength)]
    public string? Name { get; set; }

    [StringLength(UserNumberConstants.AvatarPath)]
    public string? AvatarPath { get; set; }

    [StringLength(UserNumberConstants.BackgroundPath)]
    public string? BackdropPath { get; set; }

    public long? TelegramId { get; set; }

    [StringLength(UserNumberConstants.Description)]
    public string? Description { get; set; }

    [StringLength(UserNumberConstants.TelegramUrl)]
    public string? TelegramUrl { get; set; }

    [StringLength(UserNumberConstants.StatusIcon)]
    public required string StatusIcon { get; set; }

    [StringLength(UserNumberConstants.StatusText)]
    public string? StatusText { get; set; }

    public required bool AllowAdult { get; set; }

    public required DateTime LastSeenAt { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }

    public virtual ICollection<IdentityRole<Guid>> Roles { get; set; } = [];

    object ICloneable.Clone()
    {
        return Clone();
    }

    public User Clone()
    {
        return (User)MemberwiseClone();
    }
}