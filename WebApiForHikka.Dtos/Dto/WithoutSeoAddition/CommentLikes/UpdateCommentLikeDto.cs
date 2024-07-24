using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentLikes;

[ModelMetadataType<CommentLike>]
[ExportTsInterface]
public class UpdateCommentLikeDto : ModelDto
{
    [EntityValidation<Comment>] public required Guid CommentId { get; set; }
    [EntityValidation<User>] public required Guid UserId { get; set; }
    public bool IsLiked { get; set; }
}