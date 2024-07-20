using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

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