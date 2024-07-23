using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public static class GetEmojiGroupModels
{
    public static EmojiGroup GetSample()
    {
        return new EmojiGroup
        {
            Slug = "Slug",
            Name = "Name"
        };
    }

    public static EmojiGroup GetSampleForUpdate()
    {
        return new EmojiGroup
        {
            Slug = "Slug1",
            Name = "Name1"
        };
    }

    public static CreateEmojiGroupDto GetCreateDtoSample()
    {
        return new CreateEmojiGroupDto
        {
            Name = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord()
        };
    }

    public static GetEmojiGroupDto GetGetDtoSample()
    {
        return new GetEmojiGroupDto
        {
            Name = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static EmojiGroup GetModelSample()
    {
        return new EmojiGroup
        {
            Name = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }

    public static UpdateEmojiGroupDto GetUpdateDtoSample()
    {
        return new UpdateEmojiGroupDto
        {
            Name = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            Id = new Guid()
        };
    }
}