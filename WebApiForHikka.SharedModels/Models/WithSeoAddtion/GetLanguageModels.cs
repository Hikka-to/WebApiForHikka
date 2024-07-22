using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetLanguageModels
{
    public static Language GetSample()
    {
        return new Language()
        {
            Name = "test",
            Icon = "test",
            Locale = "TE",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }
    
    public static Language GetSampleForUpdate()
    {
        return new Language
        {
            Name = "test",
            Icon = "test",
            Locale = "TE",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateLanguageDto GetCreateDtoSample()
    {
        return new CreateLanguageDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Locale = "TE",
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetLanguageDto GetGetDtoSample()
    {
        return new GetLanguageDto()
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Locale = "TE",
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateLanguageDto GetUpdateDtoSample()
    {
        return new UpdateLanguageDto()
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            Locale = "TE",
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}