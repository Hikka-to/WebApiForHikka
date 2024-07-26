using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering.Transforms;

public interface IFilteringTransform
{
    IEnumerable<IEnumerable<FilteringItem>> Transform(IEntityType entityType,
        IEnumerable<IEnumerable<FilteringItem>> items);
}