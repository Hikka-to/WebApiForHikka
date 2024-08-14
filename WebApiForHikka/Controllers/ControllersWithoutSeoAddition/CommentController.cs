using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Commentables;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class CommentController(
    ICommentService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    ICommentableService commentableService) : CrudController<
    GetCommentDto,
    UpdateCommentDto,
    CreateCommentDto,
    ICommentService,
    Comment
>(crudRelationService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateCommentDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Comment>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Parent = (await commentableService.GetAsync(dto.ParentId, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateCommentDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<Comment>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Parent = (await commentableService.GetAsync(dto.ParentId, cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}