using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Kinds;

namespace WebApiForHikka.Domain.Models;

public class Kind : ModelWithSeoAddition
{
     public required string Slug { get; set; }

     [StringLength(KindNumberConstants.HintLenght)]
     public required string Hint { get; set; }
}
