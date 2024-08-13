using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;

[ModelMetadataType(typeof(SearchHistory))]
[ExportTsInterface]
public class UpdateSearchHistoryDto : ModelDto
{
    public required string Query { get; set; }
    public required DateTime CreateAt { get; set; }
}