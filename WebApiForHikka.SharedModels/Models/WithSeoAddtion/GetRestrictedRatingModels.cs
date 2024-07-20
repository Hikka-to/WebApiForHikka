using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetRestrictedRatingModels
{
    public static RestrictedRating GetSample()
    {
        return new RestrictedRating
        {
            Name = "test",
            Hint = "test",
            Icon = "test",
            Value = 1,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static RestrictedRating GetSampleForUpdate()
    {
        return new RestrictedRating
        {
            Name = "test1",
            Hint = "test1",
            Icon = "test1",
            Value = 2,
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateRestrictedRatingDto GetCreateDtoSample()
    {
        return new CreateRestrictedRatingDto
        {
            Hint = Lorem.GetFirstWord(),
            Name = Lorem.GetFirstWord(),
            Value = 2,
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetRestrictedRatingDto GetGetDtoSample()
    {
        return new GetRestrictedRatingDto
        {
            Hint = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Name = Lorem.GetFirstWord(),
            Value = 1,
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateRestrictedRatingDto GetUpdateDtoSample()
    {
        return new UpdateRestrictedRatingDto
        {
            Hint = Lorem.GetFirstWord(),
            Name = Lorem.GetFirstWord(),
            Value = 1,
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}