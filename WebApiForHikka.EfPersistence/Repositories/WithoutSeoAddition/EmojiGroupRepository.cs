using WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class EmojiGroupRepository(HikkaDbContext dbContext)
    : CrudRepository<EmojiGroup>(dbContext), IEmojiGroupRepository;
