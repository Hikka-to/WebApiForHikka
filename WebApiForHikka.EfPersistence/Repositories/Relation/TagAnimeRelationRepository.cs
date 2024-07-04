using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;
public class TagAnimeRelationRepository(HikkaDbContext dbContext) : CrudRelationRepository<DubAnime, Dub, Anime>(dbContext), IDubAnimeRealtionRepository;
