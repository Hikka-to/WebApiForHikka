

namespace WebApiForHikka.Domain.Models;
public class User : Model
{
    public override bool IsMatch(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public override object? SortBy(string sortColumn)
    {
        throw new NotImplementedException();
    }
}
