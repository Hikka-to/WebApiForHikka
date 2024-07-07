using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;

public class AnimeVideoService(IAnimeVideoRepository repository)
    : CrudService<AnimeVideo, IAnimeVideoRepository>(repository), IAnimeVideoService;