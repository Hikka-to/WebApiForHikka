using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApiForHikka.Domain.Models;

[Index(nameof(FirstId), nameof(SecondId), IsUnique = true)]
public abstract class RelationModel<TFirst, TSecond> : Model
{
    public required Guid FirstId { get; set; }
    public required Guid SecondId { get; set; }

    [ForeignKey(nameof(FirstId))] public virtual TFirst First { get; set; }

    [ForeignKey(nameof(SecondId))] public virtual TSecond Second { get; set; }

    public void Deconstruct(out Guid firstId, out Guid secondId)
    {
        firstId = FirstId;
        secondId = SecondId;
    }
}