using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;

public abstract class ModelWithSeoAddition : Model, IModelWithSeoAddition
{
    [ForeignKey("SeoAdditionId")] public virtual required SeoAddition SeoAddition { get; set; }
}