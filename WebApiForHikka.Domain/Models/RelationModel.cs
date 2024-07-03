using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;

[PrimaryKey(nameof(Id), nameof(SecondId))]
public abstract class RelationModel<TFirst, TSecond> : Model
{
    public Guid SecondId { get; set; }

    [ForeignKey(nameof(Id))]
    public required TFirst First { get; set; }

    [ForeignKey(nameof(SecondId))]
    public required TSecond Second { get; set; }
}
