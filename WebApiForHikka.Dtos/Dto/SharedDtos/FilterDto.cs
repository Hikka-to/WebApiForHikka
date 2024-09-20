using System.ComponentModel;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;

namespace WebApiForHikka.Dtos.Dto.SharedDtos;

[ExportTsInterface]
public class FilterDto
{
    [DefaultValue("")]
    [TsDefaultValue("")]
    public string SearchTerm { get; set; } = "";

    [DefaultValue(SharedStringConstants.IdName)]
    [TsDefaultValue(SharedStringConstants.IdName)]
    public string Column { get; set; } = SharedStringConstants.IdName;

    [DefaultValue(FilterType.Strict)]
    public FilterType FilterType { get; set; } = FilterType.Strict;

    [DefaultValue(false)] public bool Negate { get; set; } = false;
}