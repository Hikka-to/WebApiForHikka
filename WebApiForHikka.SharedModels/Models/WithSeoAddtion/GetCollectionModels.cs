using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetCollectionModels
{
    public static Collection GetSample()
    {
        return new Collection
        {
            Name = "Collection Name",
            Description = "Collection Description",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Collection GetSampleForUpdate()
    {
        return new Collection
        {
            Name = "Collection Name 1",
            Description = "Collection Description 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateCollectionDto GetCreateDtoSample()
    {
        return new CreateCollectionDto
        {
            Name = "Collection Name",
            Description = "Collection Description",
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetCollectionDto GetGetDtoSample()
    {
        return new GetCollectionDto
        {
            Name = "Collection Name",
            Description = "Collection Description",
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Id = new Guid()
        };
    }

    public static UpdateCollectionDto GetUpdateDtoSample()
    {
        return new UpdateCollectionDto
        {
            Name = "Collection Name",
            Description = "Collection Description",
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}