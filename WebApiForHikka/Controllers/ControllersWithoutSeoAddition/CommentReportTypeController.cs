using AutoMapper;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class CommentReportTypeController(
    ICommentReportTypeService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : CrudController<
    GetCommentReportTypeDto,
    UpdateCommentReportTypeDto,
    CreateCommentReportTypeDto,
    ICommentReportTypeService,
    CommentReportType
>(crudRelationService, mapper, httpContextAccessor);