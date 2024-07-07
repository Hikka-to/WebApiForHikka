using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagAnimes;

public interface ITagAnimeRelationService : IRelationCrudService<TagAnime, Tag, Anime>;