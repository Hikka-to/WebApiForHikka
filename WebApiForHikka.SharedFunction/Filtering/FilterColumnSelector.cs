using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering;

public class FilterColumnSelector
{
    private static List<FilteringItem[]> GetNavigationColumns(IEntityType entityType)
    {
        List<FilteringItem[]> columns = [];
        columns.AddRange(entityType.GetProperties()
            .Where(p => (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false))
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
            where !(!navigation.FieldInfo?.IsPublic ?? true) || !(!navigation.PropertyInfo?.GetMethod?.IsPublic ?? true)
            let targetColumns = GetNavigationColumns(targetEntityType)
            from targetColumn in targetColumns
            select (FilteringItem[]) [new FilteringItem(navigation.Name, navigation.Name, navigation), ..targetColumn]);

        columns.AddRange(entityType.GetProperties()
            .Where(p => (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false))
            .Select(property => (FilteringItem[])
                [new FilteringItem(property.GetColumnName(), property.Name, property)]));

        columns.Reverse();

        return Filtering.FilterTransforms.Aggregate(columns as IEnumerable<IEnumerable<FilteringItem>>,
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
}