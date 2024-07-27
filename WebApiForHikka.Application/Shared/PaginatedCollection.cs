using System.Collections;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Shared;

public sealed record PaginatedCollection<TModel>(IReadOnlyCollection<TModel> Models, int Total)
    : IEnumerable<TModel> where TModel : class, IModel
{
    public IEnumerator<TModel> GetEnumerator()
    {
        // ReSharper disable once NotDisposedResourceIsReturned
        return Models.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}