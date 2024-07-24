using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;

[ModelMetadataType<CommentReport>]
[ExportTsInterface]
public class CreateCommentReportDto
{
    [EntityValidation<Comment>] public required Guid CommentId { get; set; }
    [EntityValidation<User>] public required Guid UserId { get; set; }
    [EntityValidation<CommentReportType>] public required Guid CommentReportTypeId { get; set; }
    public required string Body { get; set; }
}