using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NpgsqlTypes;
using WebApiForHikka.Domain;

namespace WebApiForHikka.SharedFunction.Filtering;

public class FilterColumnSelector
{
    private static List<FilteringItem[]> GetNavigationColumns(IEntityType entityType)
    {
        List<FilteringItem[]> columns = [];
        columns.AddRange(entityType.GetProperties()
            .Where(p =>
                (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false))
            .Select(property => (FilteringItem[])
                [new FilteringItem(property.GetColumnName(), property.Name, property)]));

        return columns;
    }

    public static IEnumerable<IEnumerable<FilteringItem>> GetColumns(IEntityType entityType)
    {
        List<FilteringItem[]> columns = [];
        INavigationBase[] navigations =
        [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ];

        columns.AddRange(from navigation in navigations
            let targetEntityType = navigation.TargetEntityType
            where !(!navigation.FieldInfo?.IsPublic ?? true) ||
                  !(!navigation.PropertyInfo?.GetMethod?.IsPublic ?? true)
            let targetColumns = GetNavigationColumns(targetEntityType)
            from targetColumn in targetColumns
            select (FilteringItem[])
                [new FilteringItem(navigation.Name, navigation.Name, navigation), ..targetColumn]);

        columns.AddRange(entityType.GetProperties()
            .Where(p =>
                (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false))
            .Select(property => (FilteringItem[])
                [new FilteringItem(property.GetColumnName(), property.Name, property)]));

        columns.Reverse();

        return Filtering.FilterTransforms.Aggregate(
            columns as IEnumerable<IEnumerable<FilteringItem>>,
            (current, transform) => transform.Transform(entityType, current));
    }

    public static bool IsColumnValidByReadablePath(IEntityType entityType, string readablePath)
    {
        return GetColumns(entityType).Any(c => c.GetReadablePath() == readablePath);
    }

    public static bool IsColumnValidByActualPath(IEntityType entityType, string actualPath)
    {
        return GetColumns(entityType).Any(c => c.GetActualPath() == actualPath);
    }

    public static bool TryGetColumnByReadablePath(IEntityType entityType, string readablePath,
        [NotNullWhen(true)] out IEnumerable<FilteringItem>? column)
    {
        column = GetColumns(entityType).FirstOrDefault(c => c.GetReadablePath() == readablePath);
        return column is not null;
    }

    public static bool TryGetColumnByActualPath(IEntityType entityType, string actualPath,
        [NotNullWhen(true)] out IEnumerable<FilteringItem>? column)
    {
        column = GetColumns(entityType).FirstOrDefault(c => c.GetActualPath() == actualPath);
        return column is not null;
    }

    public static IEnumerable<FilteringItem> GetColumnByReadablePath(IEntityType entityType,
        string readablePath)
    {
        return GetColumns(entityType).First(c => c.GetReadablePath() == readablePath);
    }

    public static IEnumerable<FilteringItem> GetColumnByActualPath(IEntityType entityType,
        string actualPath)
    {
        return GetColumns(entityType).First(c => c.GetActualPath() == actualPath);
    }

    public static IEnumerable<FilterType> GetFilterTypes(Type type)
    {
        if (typeof(NpgsqlTsVector).IsAssignableFrom(type))
            return
            [
                FilterType.WebSearch,
                FilterType.OrderedWebSearch,
                FilterType.DescendingOrderedWebSearch
            ];
        if (typeof(IComparable).IsAssignableFrom(Nullable.GetUnderlyingType(type) ?? type))
            return
            [
                FilterType.Strict,
                FilterType.Like,
                FilterType.InsensitiveLike,
                FilterType.Contains,
                FilterType.InsensitiveContains,
                FilterType.StartsWith,
                FilterType.EndsWith,
                FilterType.InsensitiveStartsWith,
                FilterType.InsensitiveEndsWith,
                FilterType.Bigger,
                FilterType.Smaller,
                FilterType.BiggerOrEqual,
                FilterType.SmallerOrEqual,
                FilterType.WebSearch,
                FilterType.OrderedWebSearch,
                FilterType.DescendingOrderedWebSearch
            ];

        return
        [
            FilterType.Strict,
            FilterType.Like,
            FilterType.InsensitiveLike,
            FilterType.Contains,
            FilterType.InsensitiveContains,
            FilterType.StartsWith,
            FilterType.EndsWith,
            FilterType.InsensitiveStartsWith,
            FilterType.InsensitiveEndsWith,
            FilterType.WebSearch,
            FilterType.OrderedWebSearch,
            FilterType.DescendingOrderedWebSearch
        ];
    }
}