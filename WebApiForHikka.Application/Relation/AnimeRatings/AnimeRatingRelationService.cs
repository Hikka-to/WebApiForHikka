using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeRatings;

public class AnimeRatingRelationService(IAnimeRatingRelationRepository animeRatingRelationRepository)
    : RelationCrudService<AnimeRating, User, Anime, IAnimeRatingRelationRepository>(
        animeRatingRelationRepository), IAnimeRatingRelationService;