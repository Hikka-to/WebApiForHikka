using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Relateds;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.Relation;

public class GetRelatedModels
{
    public static Related GetSample()
    {
        return new Related
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleWithoutManyToMany(),
            Second = GetAnimeGroupModels.GetSample(),
            RelatedType = GetRelatedTypeModels.GetSample()
        };
    }

    public static Related GetSampleForUpdate()
    {
        return new Related
        {
            FirstId = default,
            SecondId = default,
            First = GetAnimeModels.GetSampleForUpdateWithoutManyToMany(),
            Second = GetAnimeGroupModels.GetSampleForUpdate(),
            RelatedType = GetRelatedTypeModels.GetSampleForUpdate()
        };
    }

    public static CreateRelatedDto GetCreateDtoSample()
    {
        return new CreateRelatedDto
        {
            AnimeId = Guid.NewGuid(),
            AnimeGroupId = Guid.NewGuid(),
            RelatedTypeId = Guid.NewGuid()
        };
    }

    public static GetRelatedDto GetGetDtoSample()
    {
        return new GetRelatedDto
        {
            AnimeId = Guid.NewGuid(),
            AnimeGroupId = Guid.NewGuid(),
            RelatedTypeId = Guid.NewGuid(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateRelatedDto GetUpdateDtoSample()
    {
        return new UpdateRelatedDto
        {
            AnimeId = Guid.NewGuid(),
            AnimeGroupId = Guid.NewGuid(),
            RelatedTypeId = Guid.NewGuid(),
            Id = Guid.NewGuid()
        };
    }
}