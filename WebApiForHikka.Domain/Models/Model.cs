namespace WebApiForHikka.Domain.Models
{
    public abstract class Model : IModel
    {
        public Guid Id { get; set; }
    }
}
