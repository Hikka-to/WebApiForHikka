using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.AnimeVideoKinds;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

[Index(nameof(Name), IsUnique = true)]
public class AnimeVideoKind : Model
{
    [StringLength(AnimeVideoKindNumberConstants.NameLength)]
    public required string Name { get; set; }
}
