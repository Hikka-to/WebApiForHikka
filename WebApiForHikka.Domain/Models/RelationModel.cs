using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;

public abstract class RelationModel<TFirst, TSecond> : Model
{

    public required Guid FirstId { get; set; }
    public required Guid SecondId { get; set; }

    [ForeignKey(nameof(FirstId))]
    public  TFirst First { get; set; }

    [ForeignKey(nameof(SecondId))]
    public  TSecond Second { get; set; }
}
