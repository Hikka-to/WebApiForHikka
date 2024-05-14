using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Sources;

public class CreateSourceDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}
