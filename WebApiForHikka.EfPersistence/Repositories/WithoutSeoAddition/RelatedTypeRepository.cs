using WebApiForHikka.Application.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class RelatedTypeRepository(HikkaDbContext dbContext)
    : CrudRepository<RelatedType>(dbContext), IRelatedTypeRepository;