using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;
public class UpdateStatusDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}
