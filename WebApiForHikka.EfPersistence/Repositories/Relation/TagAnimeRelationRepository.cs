using WebApiForHikka.Application.Relation.TagAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class TagAnimeRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<TagAnime, Tag, Anime>(dbContext), ITagAnimeRelationRepository;