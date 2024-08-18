using AutoMapper;
using WebApiForHikka.Application.Relation.AnimeCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

public class AnimeCharacterController(
    IAnimeCharacterRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    AnimeCharacter,
    Anime,
    Character,
    IAnimeCharacterRelationService
>(relationService, mapper, httpContextAccessor);