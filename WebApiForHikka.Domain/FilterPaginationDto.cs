using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record FilterPaginationDto(
    string SearchTerm = "",
    int PageNumber = ShraredNumberConstatnts.DefaultPageToStartWith,
    int PageSize = ShraredNumberConstatnts.DefaultItemsInOnePage,
    string SortColumn = SharedStringConstants.IdName,
    SortOrder SortOrder = SortOrder.Asc);