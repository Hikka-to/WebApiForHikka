using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AnimeVideoKinds;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

[Index(nameof(Name), IsUnique = true)]
public class AnimeVideoKind : Model
{
    [StringLength(AnimeVideoKindNumberConstants.NameLength)]
    public required string Name { get; set; }
}