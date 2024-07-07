using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;

public abstract class ModelWithSeoAddition : Model
{
    [ForeignKey("SeoAdditionId")] public required SeoAddition SeoAddition { get; set; }
}