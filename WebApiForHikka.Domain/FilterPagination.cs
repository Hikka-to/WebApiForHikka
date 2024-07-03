using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record FilterPagination(
    string SearchTerm = "",
    int PageNumber = SharedNumberConstatnts.DefaultPageToStartWith,
    int PageSize = SharedNumberConstatnts.DefaultItemsInOnePage,
    string Column = SharedStringConstants.IdName,
    SortOrder SortOrder = SortOrder.Asc);