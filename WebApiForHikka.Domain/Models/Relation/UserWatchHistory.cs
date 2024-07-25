using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.SharedModels.Models.Relation;

public class UserWatchHistory : RelationModel<User, Episode>
{

    [Range(0, int.MaxValue)]
    public required int ProgressTime { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }


}
