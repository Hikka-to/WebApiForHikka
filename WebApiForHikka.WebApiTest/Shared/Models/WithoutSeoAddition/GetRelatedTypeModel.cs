using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetRelatedTypeModel
{
    public static RelatedType GetSample()
    {
        return new RelatedType
        {
            Name = "Name"
        };
    }

    public static RelatedType GetSampleForUpdate()
    {
        return new RelatedType
        {
            Name = "Name1"
        };
    }

    public static CreateRelatedTypeDto GetCreateDtoSample()
    {
        return new CreateRelatedTypeDto
        {
            Name = Lorem.GetFirstWord(),
        };
    }

    public static GetRelatedTypeDto GetGetDtoSample()
    {
        return new GetRelatedTypeDto
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static RelatedType GetModelSample()
    {
        return new RelatedType
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static UpdateRelatedTypeDto GetUpdateDtoSample()
    {
        return new UpdateRelatedTypeDto
        {
            Name = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }
}