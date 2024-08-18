using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeCharacters;

public interface IAnimeCharacterRelationRepository : IRelationCrudRepository<AnimeCharacter, Anime, Character>;