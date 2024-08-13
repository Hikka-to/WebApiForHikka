using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.WithSeoAddition.Characters;

public class CharacterService (ICharacterRepository repository)
    : CrudService<Character, ICharacterRepository>(repository), ICharacterService;