using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class DubAnimeRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<DubAnime, Dub, Anime>(dbContext), IDubAnimeRelationRepository;