using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class EmojiGroupController(
    IEmojiGroupService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetEmojiGroupDto,
        UpdateEmojiGroupDto,
        CreateEmojiGroupDto,
        IEmojiGroupService,
        EmojiGroup
    >(crudService, mapper, httpContextAccessor);