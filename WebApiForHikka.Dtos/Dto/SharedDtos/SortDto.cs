using System.ComponentModel;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;

namespace WebApiForHikka.Dtos.Dto.SharedDtos;

[ExportTsInterface]
public class SortDto
{
    [DefaultValue(SharedStringConstants.IdName)]
    [TsDefaultValue(SharedStringConstants.IdName)]
    public string Column { get; set; } = SharedStringConstants.IdName;

    [DefaultValue(SortOrder.Asc)] public SortOrder SortOrder { get; set; } = SortOrder.Asc;
}