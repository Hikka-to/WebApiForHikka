using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Anime : ModelWithSeoAddition
{
    public ICollection<Tag> Tags { get; } = [];
    public ICollection<TagAnime> TagAnimes { get; } = [];
}
