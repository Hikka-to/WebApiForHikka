using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Status;
public class GetStatusDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}
