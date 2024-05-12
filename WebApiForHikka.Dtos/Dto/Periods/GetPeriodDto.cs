using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

public class GetPeriodDto : GetDtoWithSeoAddition
{

    public required string Name { get; set; }
}
