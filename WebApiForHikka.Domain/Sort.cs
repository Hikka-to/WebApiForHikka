using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record Sort(
    string Column = SharedStringConstants.IdName,
    SortOrder SortOrder = SortOrder.Asc);