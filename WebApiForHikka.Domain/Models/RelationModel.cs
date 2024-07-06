using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;

[PrimaryKey(nameof(Id), nameof(SecondId))]
public abstract class RelationModel<TFirst, TSecond> : Model
{
    public required Guid SecondId { get; set; }

    [ForeignKey(nameof(Id))]
    public  TFirst First { get; set; }

    [ForeignKey(nameof(SecondId))]
    public  TSecond Second { get; set; }
}
