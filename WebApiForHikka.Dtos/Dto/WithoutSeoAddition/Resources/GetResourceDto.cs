using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Resources;

[ExportTsInterface]
public class GetResourceDto : ModelDto
{
    public string Slug {  get; set; } 
}
