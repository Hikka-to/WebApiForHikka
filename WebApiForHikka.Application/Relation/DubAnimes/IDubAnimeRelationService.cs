using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.DubAnimes;

public interface IDubAnimeRelationService : IRelationCrudService<DubAnime, Dub, Anime>;