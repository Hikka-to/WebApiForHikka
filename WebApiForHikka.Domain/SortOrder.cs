using System.Text.Json.Serialization;

namespace WebApiForHikka.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    Asc,
    Desc
}