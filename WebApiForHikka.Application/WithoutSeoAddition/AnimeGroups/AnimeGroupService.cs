using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeGroups;

public class AnimeGroupService(IAnimeGroupRepository repository)
    : CrudService<AnimeGroup, IAnimeGroupRepository>(repository),
        IAnimeGroupService
{
}