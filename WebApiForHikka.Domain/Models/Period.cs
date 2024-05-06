using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiForHikka.Constants.Periods;

namespace WebApiForHikka.Domain.Models;

public class Period : Model
{

    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }

    public Period() { }

    [ForeignKey("SeoAdditionId")]
    public required virtual SeoAddition SeoAddition { get; set; }
}
