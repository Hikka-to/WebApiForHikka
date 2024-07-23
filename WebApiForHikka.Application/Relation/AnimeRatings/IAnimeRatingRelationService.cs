using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeRatings;

public interface IAnimeRatingRelationService : IRelationCrudService<AnimeRating, User, Anime>;
