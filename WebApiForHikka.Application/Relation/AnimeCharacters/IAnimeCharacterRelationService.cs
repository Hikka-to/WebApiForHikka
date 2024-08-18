using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.AnimeCharacters;

public interface IAnimeCharacterRelationService : IRelationCrudService<AnimeCharacter, Anime, Character>;