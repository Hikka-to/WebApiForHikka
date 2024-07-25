using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;


[ExportTsInterface]
public class GetSearchHistoryDto : ModelDto
{
    public required string Query {  get; set; }
    public required DateTime CreateAt { get; set; }

}
