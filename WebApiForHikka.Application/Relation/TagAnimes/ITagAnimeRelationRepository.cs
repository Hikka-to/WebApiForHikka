using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.TagAnimes;
public interface ITagAnimeRelationRepository : IRelationCrudRepository<TagAnime>;
