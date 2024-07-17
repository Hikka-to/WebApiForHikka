using WebApiForHikka.Application.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class ExternalLinkRepository(HikkaDbContext dbContext)
    : CrudRepository<ExternalLink>(dbContext), IExternalLinkRepository;