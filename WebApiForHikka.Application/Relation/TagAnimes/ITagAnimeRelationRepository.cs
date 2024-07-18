using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagAnimes;

public interface ITagAnimeRelationRepository : IRelationCrudRepository<TagAnime, Tag, Anime>;