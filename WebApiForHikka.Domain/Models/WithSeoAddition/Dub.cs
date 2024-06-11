using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Dubs;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Dub : ModelWithSeoAddition
{
    [StringLength(DubNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(DubNumberConstants.IconLenght)]
    public string? Icon { get; set; }

    public ICollection<Anime> Animes { get; } = [];
    public ICollection<DubAnime> DubAnimes { get; } = [];
}
