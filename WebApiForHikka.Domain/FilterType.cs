using System.Text.Json.Serialization;

namespace WebApiForHikka.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterType
{
    Strict,
    Like,
    InsensitiveLike,
    Contains,
    InsensitiveContains,
    StartsWith,
    EndsWith,
    InsensitiveStartsWith,
    InsensitiveEndsWith,
    Bigger,
    Smaller,
    BiggerOrEqual,
    SmallerOrEqual,
    WebSearch,
    OrderedWebSearch,
    DescendingOrderedWebSearch
}