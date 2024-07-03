namespace WebApiForHikka.Domain.Models;
public abstract class RelationModel : Model, IModel 
{
    public Guid SecondId { get; set; }

}
