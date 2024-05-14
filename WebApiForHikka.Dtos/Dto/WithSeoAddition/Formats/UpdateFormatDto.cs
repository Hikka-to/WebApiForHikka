using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

public class UpdateFormatDto : UpdateDtoWithSeoAddition
{
    public string Name { get; set; }
}
