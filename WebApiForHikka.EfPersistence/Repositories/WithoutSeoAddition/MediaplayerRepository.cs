using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Constants.Models.Mediaplayers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class MediaplayerRepository(HikkaDbContext dbContext) : CrudRepository<Mediaplayer>(dbContext), IMediaplayerRepository;
