using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;

namespace WebApiForHikka.Dtos.Dto.SharedDtos;

[ExportTsInterface(OutputDir = "./TS/Dto/SharedDtos")]
public class FilterPaginationDto
{
    [DefaultValue("")]
    [TsDefaultValue("")]
    [Required]
    public string SearchTerm { get; set; } = "";

    [DefaultValue(SharedNumberConstatnts.DefaultPageToStartWith)]
    [Required]
    public int PageNumber { get; set; } = SharedNumberConstatnts.DefaultPageToStartWith;

    [DefaultValue(SharedNumberConstatnts.DefaultItemsInOnePage)]
    [Required]
    public int PageSize { get; set; } = SharedNumberConstatnts.DefaultItemsInOnePage;


    [DefaultValue(SharedStringConstants.IdName)]
    [TsDefaultValue(SharedStringConstants.IdName)]
    [Required]
    public string Column { get; set; } = SharedStringConstants.IdName;

    [DefaultValue(SortOrder.Asc)]
    [Required]
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
}
