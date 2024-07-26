using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering;

public class SortColumnSelector
{
    private static List<FilteringItem[]> GetNavigationColumns(
        IEntityType entityType)
    {
        List<FilteringItem[]> columns = [];
        INavigationBase[] navigations =
        [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ];

        columns.AddRange(from navigation in navigations
            where navigation.IsCollection
            select (FilteringItem[]) [new FilteringItem(navigation.Name, navigation.Name, navigation)]);

        columns.AddRange(from property in entityType.GetProperties()
            where (property.FieldInfo?.IsPublic ?? false) || (property.PropertyInfo?.GetMethod?.IsPublic ?? false)
            select (FilteringItem[]) [new FilteringItem(property.GetColumnName(), property.Name, property)]);

        return columns;
    }

    public static IEnumerable<IEnumerable<FilteringItem>> GetColumns(
        IEntityType entityType)
    {
        List<FilteringItem[]> columns = [];
        INavigationBase[] navigations =
        [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ];

        foreach (var navigation in navigations)
        {
            var targetEntityType = navigation.TargetEntityType;
            if ((!navigation.FieldInfo?.IsPublic ?? true) &&
                (navigation.PropertyInfo?.GetMethod == null || !navigation.PropertyInfo.GetMethod.IsPublic)) continue;
            var targetColumns = GetNavigationColumns(targetEntityType);
            var filteringItem = new FilteringItem(navigation.Name, navigation.Name, navigation);

            columns.AddRange(targetColumns.Where(c =>
                !navigation.IsCollection ||
                (!typeof(string).IsAssignableFrom(c.GetProperty().ClrType) &&
                 typeof(IEnumerable).IsAssignableFrom(c.GetProperty().ClrType)) ||
                typeof(bool).IsAssignableFrom(c.GetProperty().ClrType) ||
                typeof(int).IsAssignableFrom(c.GetProperty().ClrType) ||
                typeof(long).IsAssignableFrom(c.GetProperty().ClrType) ||
                typeof(double).IsAssignableFrom(c.GetProperty().ClrType) ||
                typeof(decimal).IsAssignableFrom(c.GetProperty().ClrType)).Select(targetColumn =>
                (FilteringItem[]) [filteringItem, ..targetColumn]));
            if (navigation.IsCollection) columns.Add([filteringItem]);
        }

        columns.AddRange(entityType.GetProperties()
            .Where(p => (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false))
            .Select(property => (FilteringItem[])
                [new FilteringItem(property.GetColumnName(), property.Name, property)]));

        columns.Reverse();

        return Filtering.SortTransforms.Aggregate(columns as IEnumerable<IEnumerable<FilteringItem>>,
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