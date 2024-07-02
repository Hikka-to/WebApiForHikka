using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Constants.Models.Mediaplayers;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class MediaplayerController(IMediaplayerService crudService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetMediaplayerDto,
        UpdateMediaplayerDto,
        CreateMediaplayerDto,
        IMediaplayerService,
        Mediaplayer
    >(crudService, mapper, httpContextAccessor);

