using WebApiForHikka.Application.WithSeoAddition.Formats;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class FormatRepository(HikkaDbContext dbContext) : CrudRepository<Format>(dbContext), IFormatRepository;