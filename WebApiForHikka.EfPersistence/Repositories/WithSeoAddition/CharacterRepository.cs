using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

public class CharacterRepository(HikkaDbContext dbContext) : CrudRepository<Character>(dbContext),  ICharacterRepository;
