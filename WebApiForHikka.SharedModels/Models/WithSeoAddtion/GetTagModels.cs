using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using Boolean = Faker.Boolean;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetTagModels
{
    public static Tag GetSample()
    {
        return new Tag
        {
            Alises = ["test"],
            EngName = "test",
            IsGenre = true,
            Name = "test",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Tag GetSampleForUpdate()
    {
        return new Tag
        {
            Alises = ["test1"],
            EngName = "test1",
            IsGenre = false,
            Name = "test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
            ParentTag = new Tag
            {
                Alises = ["test"],
                EngName = "test",
                IsGenre = true,
                Name = "test",
                SeoAddition = GetSeoAdditionModels.GetSample()
            }
        };
    }

    public static CreateTagDto GetCreateDtoSample()
    {
        return new CreateTagDto
        {
            Alises = Lorem.Words(2).ToList(),
            EngName = Lorem.GetFirstWord(),
            IsGenre = Boolean.Random(),
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetTagDto GetGetDtoSample()
    {
        return new GetTagDto
        {
            Alises = Lorem.Words(2).ToList(),
            EngName = Lorem.GetFirstWord(),
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            IsGenre = Boolean.Random(),
            Id = new Guid()
        };
    }

    public static Tag GetModelSample()
    {
        return new Tag
        {
            Alises = Lorem.Words(2).ToList(),
            EngName = Lorem.GetFirstWord(),
            IsGenre = Boolean.Random(),
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Id = new Guid()
        };
    }

    public static UpdateTagDto GetUpdateDtoSample()
    {
        return new UpdateTagDto
        {
            Alises = Lorem.Words(2).ToList(),
            EngName = Lorem.GetFirstWord(),
            IsGenre = Boolean.Random(),
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}