namespace WebApiForHikka.Domain.Models;

public interface IModelWithSeoAddition : IModel
{
    SeoAddition SeoAddition { get; set; }
}