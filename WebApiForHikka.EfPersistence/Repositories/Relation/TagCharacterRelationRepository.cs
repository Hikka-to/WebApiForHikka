using WebApiForHikka.Application.Relation.TagCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;
public class TagCharacterRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<TagCharacter, Tag, Character>(dbContext), ITagCharacterRelationRepository;
