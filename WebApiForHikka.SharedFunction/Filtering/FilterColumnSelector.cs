using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiForHikka.SharedFunction.Filtering;

public class FilterColumnSelector
{
    private static Dictionary<string, IPropertyBase> GetNavigationColumns(IEntityType entityType)
    {
        Dictionary<string, IPropertyBase> columns = [];

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
            foreach (var targetColumn in targetColumns)
                columns.Add($"{navigation.Name}.{targetColumn.Key}", targetColumn.Value);
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