using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record FilterPaginationDto(
    string SearchTerm = "",
    int PageNumber = SharedNumberConstatnts.DefaultPageToStartWith,
    int PageSize = SharedNumberConstatnts.DefaultItemsInOnePage,
    string SortColumn = SharedStringConstants.IdName,
    SortOrder SortOrder = SortOrder.Asc);