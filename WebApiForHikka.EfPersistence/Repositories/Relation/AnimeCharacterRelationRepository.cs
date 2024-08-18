using WebApiForHikka.Application.Relation.AnimeCharacters;
using WebApiForHikka.Application.Relation.TagCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class AnimeCharacterRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<AnimeCharacter, Anime, Character>(dbContext), IAnimeCharacterRelationRepository;