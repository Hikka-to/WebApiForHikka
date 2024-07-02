using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Constants.Models.RestrictedRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class RestrictedRatingRepository(HikkaDbContext dbContext) : CrudRepository<RestrictedRating>(dbContext), IRestrictedRatingRepository;
