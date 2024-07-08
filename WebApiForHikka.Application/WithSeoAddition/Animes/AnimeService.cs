using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Animes;

public class AnimeService(IAnimeRepository repository)
    : CrudService<Anime, IAnimeRepository>(repository), IAnimeService;