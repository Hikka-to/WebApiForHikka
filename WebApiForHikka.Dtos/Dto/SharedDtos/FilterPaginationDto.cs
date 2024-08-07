using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.SharedDtos;

[ExportTsInterface]
public class FilterPaginationDto 
{
    [DefaultValue(SharedNumberConstatnts.DefaultPageToStartWith)]
    [Range(SharedNumberConstatnts.MinPageNumber, int.MaxValue)]
    [Required]
    public int PageNumber { get; set; } = SharedNumberConstatnts.DefaultPageToStartWith;

    [DefaultValue(SharedNumberConstatnts.DefaultItemsInOnePage)]
    [Range(SharedNumberConstatnts.MinPageSize, SharedNumberConstatnts.MaxPageSize)]
    [Required]
    public int PageSize { get; set; } = SharedNumberConstatnts.DefaultItemsInOnePage;


    public IEnumerable<IEnumerable<FilterDto>> Filters { get; set; } = [];


    public IEnumerable<SortDto> Sorts { get; set; } = [];
}