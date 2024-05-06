using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Dto.Periods;

public class GetPeriodDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public GetSeoAdditionDto SeoAdditionDto { get; set; }
}
