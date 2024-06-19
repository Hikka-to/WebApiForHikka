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
    public string SearchTerm = "";

    [DefaultValue(SharedNumberConstatnts.DefaultPageToStartWith)]
    [Required]
    public int PageNumber = SharedNumberConstatnts.DefaultPageToStartWith;

    [DefaultValue(SharedNumberConstatnts.DefaultItemsInOnePage)]
    [Required]
    public int PageSize = SharedNumberConstatnts.DefaultItemsInOnePage;


    [DefaultValue(SharedStringConstants.IdName)]
    [TsDefaultValue(SharedStringConstants.IdName)]
    [Required]
    public string SortColumn = SharedStringConstants.IdName;

    [DefaultValue(SortOrder.Asc)]
    [Required]
    public SortOrder SortOrder = SortOrder.Asc;
}
