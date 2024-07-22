using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetLanguageMediaplayerModels
{
    public static LanguageMediaplayer GetSample()
    {
        return new LanguageMediaplayer()
        {
            Mediaplayer = GetMediaplayerModels.GetSample(),
            Language = GetLanguageModels.GetSample(),
            Episode = GetEpisodeModels.GetSample(),
            Format = GetFormatModels.GetSample(),
            Url = "test",
            FileId = "test",
            StartIntro = 0,
            EndIntro = null,
            StartEnding = null,
            EndEnding = null,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static LanguageMediaplayer GetSampleForUpdate()
    {
        return new LanguageMediaplayer
        {
            Mediaplayer = GetMediaplayerModels.GetSample(),
            Language = GetLanguageModels.GetSample(),
            Episode = GetEpisodeModels.GetSample(),
            Format = GetFormatModels.GetSample(),
            Url = "test",
            FileId = "test",
            StartIntro = 0,
            EndIntro = null,
            StartEnding = null,
            EndEnding = null,
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateLanguageMediaplayerDto GetCreateDtoSample()
    {
        return new CreateLanguageMediaplayerDto()
        {
            MediaplayerId = Guid.NewGuid(),
            LanguageId = Guid.NewGuid(),
            EpisodeId = Guid.NewGuid(),
            FormatId = Guid.NewGuid(),
            Url = "test",
            FileId = "test",
            StartIntro = 0,
            EndIntro = null,
            StartEnding = null,
            EndEnding = null,
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetLanguageMediaplayerDto GetGetDtoSample()
    {
        return new GetLanguageMediaplayerDto()
        {
            Mediaplayer = GetMediaplayerModels.GetGetDtoSample(),
            Language = GetLanguageModels.GetGetDtoSample(),
            Episode = GetEpisodeModels.GetGetDtoSample(),
            Format = GetFormatModels.GetGetDtoSample(),
            Url = "test",
            FileId = "test",
            StartIntro = 0,
            EndIntro = null,
            StartEnding = null,
            EndEnding = null,
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateLanguageMediaplayerDto GetUpdateDtoSample()
    {
        return new UpdateLanguageMediaplayerDto()
        {
            MediaplayerId = Guid.NewGuid(),
            LanguageId = Guid.NewGuid(),
            EpisodeId = Guid.NewGuid(),
            FormatId = Guid.NewGuid(),
            Url = "test",
            FileId = "test",
            StartIntro = 0,
            EndIntro = null,
            StartEnding = null,
            EndEnding = null,

            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}