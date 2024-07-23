using WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.EmojiGroups;

public class EmojiGroupServiceTest : SharedServiceTest<EmojiGroup, EmojiGroupService>
{
    protected override EmojiGroup GetSample()
    {
        return GetEmojiGroupModels.GetSample();
    }

    protected override EmojiGroup GetSampleForUpdate()
    {
        return GetEmojiGroupModels.GetSampleForUpdate();
    }

    protected override EmojiGroupService GetService(HikkaDbContext hikkaDbContext)
    {
        return new EmojiGroupService(new EmojiGroupRepository(hikkaDbContext));
    }
}