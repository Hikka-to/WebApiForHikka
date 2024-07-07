using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetRestrictedRatingModels
{
    public static RestrictedRating GetSample() => new()
    {
        Name = "test",
        Hint = "test",
        Icon = "test",
        Value = 1,
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static RestrictedRating GetSampleForUpdate() => new()
    {
        Name = "test1",
        Hint = "test1",
        Icon = "test1",
        Value = 2,
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
    public static CreateRestrictedRatingDto GetCreateDtoSample()
    {
        return new CreateRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 2,
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetRestrictedRatingDto GetGetDtoSample()
    {
        return new GetRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 1,
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }
    public static UpdateRestrictedRatingDto GetUpdateDtoSample()
    {
        return new UpdateRestrictedRatingDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord(),
            Value = 1,
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
