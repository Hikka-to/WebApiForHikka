using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Countries;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Countries;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Countries")]
public class CreateCountryDto : CreateDtoWithSeoAddition
{

    [StringLength(CountryNumberConstants.NameLenght)]
    public required string Name { get; set; }
    [StringLength(CountryNumberConstants.IconLenght)]
    public required string Icon { get; set; }
    
    
}