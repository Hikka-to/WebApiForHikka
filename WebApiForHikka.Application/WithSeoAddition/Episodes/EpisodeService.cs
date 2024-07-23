using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Episodes;

public class EpisodeService(IEpisodeRepository repository)
    : CrudService<Episode, IEpisodeRepository>(repository), IEpisodeService;