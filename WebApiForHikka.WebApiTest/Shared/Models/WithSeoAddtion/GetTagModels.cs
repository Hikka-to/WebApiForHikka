using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetTagModels
{
    public static Tag GetSample() => new()
    {
        Alises = ["test"],
        EngName = "test",
        IsGenre = true,
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Tag GetSampleForUpdate() => new()
    {
        Alises = ["test1"],
        EngName = "test1",
        IsGenre = false,
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        ParentTag = new()
        {
            Alises = ["test"],
            EngName = "test",
            IsGenre = true,
            Name = "test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
    };

    public static CreateTagDto GetCreateDtoSample()
    {
        return new CreateTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetTagDto GetGetDtoSample()
    {
        return new GetTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            IsGenre = Faker.Boolean.Random(),
            Id = new Guid(),
        };
    }

    public static Tag GetModelSample()
    {
        return new Tag()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Id = new Guid(),
        };
    }

    public static UpdateTagDto GetUpdateDtoSample()
    {
        return new UpdateTagDto()
        {
            Alises = Faker.Lorem.Words(2).ToList(),
            EngName = Faker.Lorem.GetFirstWord(),
            IsGenre = Faker.Boolean.Random(),
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
