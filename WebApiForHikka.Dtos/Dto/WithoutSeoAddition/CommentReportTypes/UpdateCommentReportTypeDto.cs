using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;

[ModelMetadataType<CommentReportType>]
[ExportTsInterface]
public class UpdateCommentReportTypeDto : ModelDto
{
    public required string Slug { get; set; }
}