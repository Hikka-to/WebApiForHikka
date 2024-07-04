using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class DubAnimeRelationRepository(HikkaDbContext dbContext) : CrudRelationRepository<DubAnime>(dbContext), IDubAnimeRealtionRepository;
