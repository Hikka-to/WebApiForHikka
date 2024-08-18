using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetAnimeCharacterModels
{ public static AnimeCharacter GetSample()
    {
        return new AnimeCharacter
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleWithoutManyToMany(),
            Second = GetCharacterModels.GetSampleWithoutManyToMany()
        };
    }

    public static AnimeCharacter GetSampleForUpdate()
    {
        return new AnimeCharacter
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleForUpdate(),
            Second = GetCharacterModels.GetSample()
        };
    }
}
