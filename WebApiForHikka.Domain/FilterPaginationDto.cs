
using WebApiForHikka.Constants.Strings;

namespace WebApiForHikka.Domain;

public sealed record FilterPaginationDto(
    string SearchTerm = "",
    int PageNumber = 1,
    int PageSize = 50,
    string SortColumn = SharedStringsConstants.IdName,
    SortOrder SortOrder = SortOrder.Asc);