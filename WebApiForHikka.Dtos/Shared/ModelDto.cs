using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Shared/")]
public abstract class ModelDto
{
    [Required] public virtual Guid Id { get; set; }
}