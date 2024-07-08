using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetSeoAdditionModels
{
    public static SeoAddition GetSample()
    {
        return new SeoAddition
        {
            Description = "Test",
            Slug = "Test",
            Title = "Test",
            Image = "Test",
            ImageAlt = "Test",
            SocialImage = "Test",
            SocialImageAlt = "Test"
        };
    }

    public static SeoAddition GetSampleForUpdate()
    {
        return new SeoAddition
        {
            Description = "Test1",
            Slug = "Test1",
            Title = "Test1",
            Image = "Test1",
            ImageAlt = "Test1",
            SocialImage = "Test1",
            SocialImageAlt = "Test1"
        };
    }

    public static CreateSeoAdditionDto GetCreateDtoSample()
    {
        return new CreateSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Lorem.GetFirstWord()
        };
    }

    public static SeoAddition GetModelSample()
    {
        return new SeoAddition
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static UpdateSeoAdditionDto GetUpdateDtoSample()
    {
        return new UpdateSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static GetSeoAdditionDto GetGetDtoSample()
    {
        return new GetSeoAdditionDto
        {
            Description = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Title = Lorem.GetFirstWord(),
            Image = Lorem.GetFirstWord(),
            ImageAlt = Lorem.GetFirstWord(),
            SocialImage = Lorem.GetFirstWord(),
            SocialImageAlt = Lorem.GetFirstWord(),
            SocialTitle = Lorem.GetFirstWord(),
            SocialType = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }
}