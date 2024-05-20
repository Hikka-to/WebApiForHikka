using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Sources;

public class UpdateSourceDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}