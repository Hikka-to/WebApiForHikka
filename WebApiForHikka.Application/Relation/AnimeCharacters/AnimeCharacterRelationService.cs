using WebApiForHikka.Application.Relation.AnimeCharacters;
using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeCharacters;

public class AnimeCharacterRelationService(IAnimeCharacterRelationRepository animeRatingRelationRepository)
    : RelationCrudService<AnimeCharacter, Anime, Character, IAnimeCharacterRelationRepository>(
        animeRatingRelationRepository), IAnimeCharacterRelationService;