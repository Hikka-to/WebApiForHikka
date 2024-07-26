using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record FilterPagination(
    int PageNumber = SharedNumberConstatnts.DefaultPageToStartWith,
    int PageSize = SharedNumberConstatnts.DefaultItemsInOnePage)
{
    public IEnumerable<IEnumerable<Filter>> Filters { get; init; } = [];
    public IEnumerable<Sort> Sorts { get; init; } = [];
}