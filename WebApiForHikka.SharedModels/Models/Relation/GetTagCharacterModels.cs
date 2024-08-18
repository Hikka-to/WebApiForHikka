using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class GetTagCharacterModels
{
public static TagCharacter GetSample()
    {
        return new TagCharacter
        {
            FirstId = default,
            SecondId = default,
            First = GetTagModels.GetSample(),
            Second = GetCharacterModels.GetSample()
        };
    }

    public static TagCharacter GetSampleForUpdate()
    {
        return new TagCharacter
        {
            FirstId = default,
            SecondId = default,
            First = GetTagModels.GetSampleForUpdate(),
            Second = GetCharacterModels.GetSampleForUpdate()
        };
    }
}
