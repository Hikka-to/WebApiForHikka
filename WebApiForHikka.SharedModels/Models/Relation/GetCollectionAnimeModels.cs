using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetCollectionAnimeModels
{
    public static CollectionAnime GetSample()
    {
        return new CollectionAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetCollectionModels.GetSample(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }

    public static CollectionAnime GetSampleForUpdate()
    {
        return new CollectionAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetCollectionModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleForUpdateWithoutManyToMany()
        };
    }
}