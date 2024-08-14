using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class CommentReportController(
    ICommentReportService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    ICommentService commentService,
    IUserService userService,
    ICommentReportTypeService commentReportTypeService) : CrudController<
    GetCommentReportDto,
    UpdateCommentReportDto,
    CreateCommentReportDto,
    ICommentReportService,
    CommentReport
>(crudRelationService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateCommentReportDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<CommentReport>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Comment = (await commentService.GetAsync(dto.CommentId, cancellationToken))!;
        model.CommentReportType =
            (await commentReportTypeService.GetAsync(dto.CommentReportTypeId, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateCommentReportDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<CommentReport>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Comment = (await commentService.GetAsync(dto.CommentId, cancellationToken))!;
        model.CommentReportType =
            (await commentReportTypeService.GetAsync(dto.CommentReportTypeId, cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}