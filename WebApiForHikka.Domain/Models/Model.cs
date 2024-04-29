namespace WebApiForHikka.Domain.Models;

public abstract class Model
{
    public Guid Id { get; set; }


    public abstract bool IsMatch(string searchTerm);
    public abstract object? SortBy(string sortColumn);

}