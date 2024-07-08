using WebApiForHikka.Application.Formats;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class FormatRepository(HikkaDbContext dbContext) : CrudRepository<Format>(dbContext), IFormatRepository;