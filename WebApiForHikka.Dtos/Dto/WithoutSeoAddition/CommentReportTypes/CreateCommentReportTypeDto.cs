using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;

[ModelMetadataType<CommentReportType>]
[ExportTsInterface]
public class CreateCommentReportTypeDto
{
    public required string Slug { get; set; }
}