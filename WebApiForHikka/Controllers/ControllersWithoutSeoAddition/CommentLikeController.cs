using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

public class CommentLikeController(
    ICommentLikeService crudRelationService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    ICommentService commentService,
    IUserService userService) : CrudController<
    GetCommentLikeDto,
    UpdateCommentLikeDto,
    CreateCommentLikeDto,
    ICommentLikeService,
    CommentLike
>(crudRelationService, mapper, httpContextAccessor)
{
    public override async Task<IActionResult> Create([FromBody] CreateCommentLikeDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequest(new ThingsToValidateBase());
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<CommentLike>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Comment = (await commentService.GetAsync(dto.CommentId, cancellationToken))!;

        var createdId = await CrudService.CreateAsync(model, cancellationToken);

        return Ok(new CreateResponseDto { Id = createdId });
    }

    public override async Task<IActionResult> Put([FromBody] UpdateCommentLikeDto dto,
        CancellationToken cancellationToken)
    {
        var errorEndPoint = ValidateRequestForUpdateEndPoint(new ThingsToValidateForUpdate
        {
            UpdateDto = dto
        });
        if (errorEndPoint.IsError) return errorEndPoint.GetError();

        var model = Mapper.Map<CommentLike>(dto);

        model.User = (await userService.GetAsync(dto.UserId, cancellationToken))!;
        model.Comment = (await commentService.GetAsync(dto.CommentId, cancellationToken))!;

        await CrudService.UpdateAsync(model, cancellationToken);

        return NoContent();
    }
}