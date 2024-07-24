using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;

[ModelMetadataType<CommentReport>]
[ExportTsInterface]
public class UpdateCommentReportDto : ModelDto
{
    [EntityValidation<Comment>] public required Guid CommentId { get; set; }
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<CommentReportType>] public required Guid CommentReportTypeId { get; set; }
    public string? Body { get; set; }
}