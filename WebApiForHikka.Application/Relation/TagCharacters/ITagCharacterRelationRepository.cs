using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagCharacters;

public interface ITagCharacterRelationRepository : IRelationCrudRepository<TagCharacter, Tag, Character>;
