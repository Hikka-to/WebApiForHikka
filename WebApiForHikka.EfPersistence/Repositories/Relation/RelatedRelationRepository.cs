using WebApiForHikka.Application.Relation.Relateds;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class RelatedRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<Related, Anime, AnimeGroup>(dbContext), IRelatedRelationRepository;