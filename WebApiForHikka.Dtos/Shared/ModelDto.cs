using System.ComponentModel.DataAnnotations;

namespace WebApiForHikka.Dtos.Shared;

public abstract class ModelDto
{
    [Required]
    public Guid Id { get; set; }
}
