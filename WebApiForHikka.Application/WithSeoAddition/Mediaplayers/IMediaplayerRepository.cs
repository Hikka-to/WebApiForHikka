using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Mediaplayers;

public interface IMediaplayerRepository : ICrudRepository<Mediaplayer>;
