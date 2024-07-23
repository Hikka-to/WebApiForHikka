using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;

public class EmojiGroupService (IEmojiGroupRepository repository)
    : CrudService<EmojiGroup, IEmojiGroupRepository>(repository), IEmojiGroupService;
