using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering;

public class SortColumnSelector
{
    private static Dictionary<string, IPropertyBase> GetNavigationColumns(IEntityType entityType)
    {
        Dictionary<string, IPropertyBase> columns = [];
        INavigationBase[] navigations =
        [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ];

        foreach (var navigation in navigations)
            if (navigation.IsCollection)
                columns.Add(navigation.Name, navigation);

        foreach (var property in entityType.GetProperties().Where(p =>
                     (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false)))
            columns.Add(property.Name, property);

        return columns;
    }

    public static IDictionary<string, IPropertyBase> GetColumns(IEntityType entityType)
    {
        Dictionary<string, IPropertyBase> columns = [];
        INavigationBase[] navigations =
        [
            ..entityType.GetNavigations(),
            ..entityType.GetSkipNavigations()
        ];

        foreach (var navigation in navigations)
        {
            var targetEntityType = navigation.TargetEntityType;
            if ((!navigation.FieldInfo?.IsPublic ?? true) &&
                (!navigation.PropertyInfo?.GetMethod?.IsPublic ?? true)) continue;
            var targetColumns = GetNavigationColumns(targetEntityType);

            foreach (var targetColumn in targetColumns.Where(c =>
                         !navigation.IsCollection ||
                         (!typeof(string).IsAssignableFrom(c.Value.ClrType) &&
                          typeof(IEnumerable).IsAssignableFrom(c.Value.ClrType)) ||
                         typeof(bool).IsAssignableFrom(c.Value.ClrType) ||
                         typeof(int).IsAssignableFrom(c.Value.ClrType) ||
                         typeof(long).IsAssignableFrom(c.Value.ClrType) ||
                         typeof(double).IsAssignableFrom(c.Value.ClrType) ||
                         typeof(decimal).IsAssignableFrom(c.Value.ClrType)))
                columns.Add($"{navigation.Name}.{targetColumn.Key}", targetColumn.Value);
            if (navigation.IsCollection) columns.Add(navigation.Name, navigation);
        }

        foreach (var property in entityType.GetProperties().Where(p =>
                     (p.FieldInfo?.IsPublic ?? false) || (p.PropertyInfo?.GetMethod?.IsPublic ?? false)))
            columns.Add(property.Name, property);

        return columns.Reverse().ToDictionary();
    }

    public static bool IsColumnValid(IEntityType entityType, string columnName)
    {
        return GetColumns(entityType).ContainsKey(columnName);
    }
}