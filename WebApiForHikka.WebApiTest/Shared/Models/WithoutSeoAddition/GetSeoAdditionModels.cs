using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAdditions;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetSeoAdditionModels 
{
    public static SeoAddition GetSample()
    {
        return new SeoAddition()
        {
            Description = "Test",
            Slug = "Test",
            Title = "Test",
            Image = "Test",
            ImageAlt = "Test",
            SocialImage = "Test",
            SocialImageAlt = "Test",
        };
    }
    public static SeoAddition GetSampleForUpdate() => new()
    {
        Description = "Test1",
        Slug = "Test1",
        Title = "Test1",
        Image = "Test1",
        ImageAlt = "Test1",
        SocialImage = "Test1",
        SocialImageAlt = "Test1",
    };

    public static CreateSeoAdditionDto GetCreateDtoSample()
    {
        return new CreateSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
        };
    }

    public static SeoAddition GetModelSample()
    {
        return new SeoAddition()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    public static UpdateSeoAdditionDto GetUpdateDtoSample()
    {
        return new UpdateSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }

    public static GetSeoAdditionDto GetGetDtoSample()
    {
        return new GetSeoAdditionDto()
        {
            Description = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            Title = Faker.Lorem.GetFirstWord(),
            Image = Faker.Lorem.GetFirstWord(),
            ImageAlt = Faker.Lorem.GetFirstWord(),
            SocialImage = Faker.Lorem.GetFirstWord(),
            SocialImageAlt = Faker.Lorem.GetFirstWord(),
            SocialTitle = Faker.Lorem.GetFirstWord(),
            SocialType = Faker.Lorem.GetFirstWord(),
            Id = new Guid(),
        };
    }
}
