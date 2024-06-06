using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Mediaplayers;


public class MediaplayerService(IMediaplayerRepository repository) : CrudService<Mediaplayer, IMediaplayerRepository>(repository), IMediaplayerService;
