using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;


[ModelMetadataType(typeof(SearchHistory))]
[ExportTsInterface]
public class CreateSearchHistoryDto
{
    public required string Query { get; set; }    
}
