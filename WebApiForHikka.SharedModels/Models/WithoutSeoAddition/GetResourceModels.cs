using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
public class GetResourceModels 
{
    public static Resource GetSample()
    {
        return new Resource
        {
            Slug = "reds"
        };
    }

    public static Resource GetSampleForUpdate()
    {
        return new Resource
        {
            Slug = "test1"
        };
    }

    public static CreateResourceDto GetCreateDtoSample()
    {
        return new CreateResourceDto
        {
            Slug = "test"
        };
    }

    public static GetResourceDto GetGetDtoSample()
    {
        return new GetResourceDto
        {
            Id = Guid.NewGuid(),
            Slug = "test12"

        };
    }

    public static UpdateResourceDto GetUpdateDtoSample()
    {
        return new UpdateResourceDto
        {
            Id = Guid.NewGuid(),
            Slug = "test231"
            
        };
    }
}
