using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.Shared;

[ExportTsInterface(OutputDir = "./TS/Shared/")]
public abstract class ModelDto
{
    [Required]
    public Guid Id { get; set; }
}