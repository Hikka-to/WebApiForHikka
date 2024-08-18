using AutoMapper;
using WebApiForHikka.Application.Relation.TagCharacters;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.WebApi.Shared.RelationController;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

public class TagCharacterController(
    ITagCharacterRelationService relationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : RelationCrudController<
    TagCharacter,
    Tag,
    Character,
    ITagCharacterRelationService
>(relationService, mapper, httpContextAccessor);
