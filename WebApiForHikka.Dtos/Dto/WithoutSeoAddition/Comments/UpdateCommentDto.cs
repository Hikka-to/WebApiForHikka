using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Comments;

[ModelMetadataType(typeof(Comment))]
[ExportTsInterface]
public class UpdateCommentDto : ModelDto
{
    public required string Body { get; set; }
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<Commentable>] public required Guid ParentId { get; set; }
}