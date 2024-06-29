using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.SeoAdditions;


[ExportTsInterface(OutputDir = "./TS/Dto/SeoAddition")]
public class GetSeoAdditionDto : ModelDto
{
    public required string Slug { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public string? Image { get; set; }

    public string? ImageAlt { get; set; }

    public string? SocialTitle { get; set; }

    public string? SocialType { get; set; } // Consider using an enum type here if you have specific values

    public string? SocialImage { get; set; }

    public string? SocialImageAlt { get; set; }
}