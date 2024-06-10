using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class TagAnime : Model
{
    public required Guid TagId;

    public required Guid AnimeId;

}
