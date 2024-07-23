using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;

public interface IEmojiGroupRepository : ICrudRepository<EmojiGroup>;
