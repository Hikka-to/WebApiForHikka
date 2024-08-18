using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.TagCharacters;

public interface ITagCharacterRelationService  : IRelationCrudService<TagCharacter, Tag, Character>;
