using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering;

public record FilteringItem(
    string ReadableName,
    string ActualName,
    IPropertyBase Property
);