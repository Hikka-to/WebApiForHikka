using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;

public class AnimeVideoKindService(IAnimeVideoKindRepository repository)
    : CrudService<AnimeVideoKind, IAnimeVideoKindRepository>(repository),
    IAnimeVideoKindService;
