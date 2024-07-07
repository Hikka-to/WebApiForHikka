using WebApiForHikka.Application.Sources;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class SourceRepository(HikkaDbContext dbContext) : CrudRepository<Source>(dbContext), ISourceRepository;