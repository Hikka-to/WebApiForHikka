using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetTagAnimeModels
{
    public static TagAnime GetSample()
    {
        return new TagAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetTagModels.GetSample(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }

    public static TagAnime GetSampleForUpdate()
    {
        return new TagAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetTagModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleForUpdateWithoutManyToMany()
        };
    }
}