using WebApiForHikka.Application.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class AlternativeNameRepository(HikkaDbContext dbContext)
    : CrudRepository<AlternativeName>(dbContext), IAlternativeNameRepository;