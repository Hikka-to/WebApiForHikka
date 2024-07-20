using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

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