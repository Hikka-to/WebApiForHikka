using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Dtos.Dto.SharedDtos;

[ExportTsInterface]
public class FilterPaginationDto
{
    [DefaultValue(SharedNumberConstatnts.DefaultPageToStartWith)]
    [Required]
    public int PageNumber { get; set; } = SharedNumberConstatnts.DefaultPageToStartWith;

    [DefaultValue(SharedNumberConstatnts.DefaultItemsInOnePage)]
    [Required]
    public int PageSize { get; set; } = SharedNumberConstatnts.DefaultItemsInOnePage;

    public IEnumerable<IEnumerable<FilterDto>> Filters { get; set; } = [];
    public IEnumerable<SortDto> Sorts { get; set; } = [];
}