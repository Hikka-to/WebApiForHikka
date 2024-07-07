using Microsoft.EntityFrameworkCore;

namespace WebApiForHikka.Domain.Models;

[PrimaryKey(nameof(Id))]
public abstract class Model : IModel
{
    public Guid Id { get; set; }
}
