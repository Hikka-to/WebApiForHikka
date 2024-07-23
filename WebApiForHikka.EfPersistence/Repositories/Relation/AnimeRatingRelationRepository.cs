using WebApiForHikka.Application.Relation.AnimeRatings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class AnimeRatingRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<AnimeRating, User, Anime>(dbContext), IAnimeRatingRelationRepository;