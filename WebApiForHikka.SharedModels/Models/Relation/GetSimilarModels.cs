using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetSimilarModels
{
    public static Similar GetSample()
    {
        return new Similar
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleWithoutManyToMany(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }

    public static Similar GetSampleForUpdate()
    {
        return new Similar
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleForUpdateWithoutManyToMany(),
            Second = GetAnimeModels.GetSampleForUpdateWithoutManyToMany()
        };
    }
}