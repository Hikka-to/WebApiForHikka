using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.Relation;

public class GetDubAnimeModels
{
    public static DubAnime GetSample()
    {
        return new DubAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetDubModels.GetSample(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }

    public static DubAnime GetSampleForUpdate()
    {
        return new DubAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetDubModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleForUpdateWithoutManyToMany()
        };
    }
}