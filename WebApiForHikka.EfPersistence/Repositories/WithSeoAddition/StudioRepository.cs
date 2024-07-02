using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithSeoAddition.Studios;
using WebApiForHikka.Constants.Models.Studios;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class StudioRepository(HikkaDbContext dbContext) : CrudRepository<Studio>(dbContext), IStudioRepository;
