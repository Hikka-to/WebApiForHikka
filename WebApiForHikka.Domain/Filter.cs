using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain;

public sealed record Filter(
    string SearchTerm = "",
    string Column = SharedStringConstants.IdName,
    bool IsStrict = false);