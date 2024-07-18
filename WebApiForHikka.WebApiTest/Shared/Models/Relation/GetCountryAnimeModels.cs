using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.Relation;

public class GetCountryAnimeModels
{
    public static CountryAnime GetSample()
    {
        return new CountryAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetCountryModels.GetSample(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }

    public static CountryAnime GetSampleForUpdate()
    {
        return new CountryAnime
        {
            FirstId = default,
            SecondId = default,
            First = GetCountryModels.GetSampleForUpdate(),
            Second = GetAnimeModels.GetSampleWithoutManyToMany()
        };
    }
}