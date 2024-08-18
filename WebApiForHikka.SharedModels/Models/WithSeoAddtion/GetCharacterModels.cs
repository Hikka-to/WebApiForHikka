using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.MyDataFakers;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetCharacterModels
{
    public static Character GetSample()
    {
        return new Character
        {
            Name = "Test Character",
            RomajiName = "Tesuto Kyarakuta",
            NativeName = "テストキャラクター",
            ImagePath = "test_image.jpg",
            Animes = [],
            SeoAddition = GetSeoAdditionModels.GetSample(),
            CreatedAt = DateTime.Today,
            UpdatedAt = DateTime.UtcNow,
            Tags = []
        };
    }

    public static Character GetSampleForUpdate()
    {
        return new Character
        {
            Name = "Updated Character",
            RomajiName = "Koushin Kyarakuta",
            NativeName = "更新キャラクター",
            ImagePath = "updated_image.jpg",
            Animes = [GetAnimeModels.GetSample()],
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
            CreatedAt = DateTime.Today,
            UpdatedAt = DateTime.UtcNow,
            Tags = [GetTagModels.GetSample()]
        };
    }

    public static CreateCharacterDto GetCreateDtoSample()
    {
        return new CreateCharacterDto
        {
            Name = Lorem.GetFirstWord(),
            RomajiName = Lorem.GetFirstWord(),
            NativeName = Lorem.GetFirstWord(),
            AlternativeName = Lorem.GetFirstWord(),
            Animes = [Guid.NewGuid()],
            Tags = [Guid.NewGuid()],
            Image = MyDataFaker.GetFakeImage(),
            Description = Lorem.Sentence(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetCharacterDto GetGetDtoSample()
    {
        return new GetCharacterDto
        {
            Id = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            RomajiName = Lorem.GetFirstWord(),
            NativeName = Lorem.GetFirstWord(),
            AlternativeName = Lorem.GetFirstWord(),
            Animes = [GetAnimeModels.GetLightAnimeDto()],
            Tags = [GetTagModels.GetGetDtoSample()],
            ImageUrl = Lorem.GetFirstWord() + ".jpg",
            Description = Lorem.Sentence(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
public static GetLightCharacterDto GetGetLightDtoSample()
    {
        return new GetLightCharacterDto
        {
            Id = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            RomajiName = Lorem.GetFirstWord(),
            NativeName = Lorem.GetFirstWord(),
            AlternativeName = Lorem.GetFirstWord(),
            Tags = [GetTagModels.GetGetDtoSample()],
            ImageUrl = Lorem.GetFirstWord() + ".jpg",
            Description = Lorem.Sentence(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static Character GetModelSample()
    {
        return new Character
        {
            Id = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            RomajiName = Lorem.GetFirstWord(),
            NativeName = Lorem.GetFirstWord(),
            AlternativeName = Lorem.GetFirstWord(),
            Animes = [GetAnimeModels.GetSample()],
            ImagePath = Lorem.GetFirstWord() + ".jpg",
            Description = Lorem.Sentence(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public static UpdateCharacterDto GetUpdateDtoSample()
    {
        return new UpdateCharacterDto
        {
            Id = Guid.NewGuid(),
            Name = Lorem.GetFirstWord(),
            RomajiName = Lorem.GetFirstWord(),
            NativeName = Lorem.GetFirstWord(),
            AlternativeName = Lorem.GetFirstWord(),
            Animes = [Guid.NewGuid()],
            Tags = [Guid.NewGuid()],
            Image = MyDataFaker.GetFakeImage(),
            Description = Lorem.Sentence(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample()
        };
    }
    
}