using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.Relation;

public class Notification : RelationModel<User, Anime>
{
    public required virtual Resource Resource { get; set; }

    public required DateTime CreatedAt { get; set; }
}
