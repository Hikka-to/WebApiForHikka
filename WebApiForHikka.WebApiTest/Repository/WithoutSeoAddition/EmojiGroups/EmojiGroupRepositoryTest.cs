using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.EmojiGroups;

public class EmojiGroupRepositoryTest : SharedRepositoryTest<EmojiGroup, EmojiGroupRepository>
{
    protected override EmojiGroup GetSample()
    {
        return GetEmojiGroupModels.GetSample();
    }

    protected override EmojiGroup GetSampleForUpdate()
    {
        return GetEmojiGroupModels.GetSampleForUpdate();
    }

    protected override EmojiGroupRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new EmojiGroupRepository(hikkaDbContext);
    }
}