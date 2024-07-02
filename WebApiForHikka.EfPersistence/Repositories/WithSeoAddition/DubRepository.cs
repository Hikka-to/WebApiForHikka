using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Constants.Models.Dubs;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class DubRepository(HikkaDbContext dbContext) : CrudRepository<Dub>(dbContext), IDubRepository;
