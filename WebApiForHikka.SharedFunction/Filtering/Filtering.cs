using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiForHikka.SharedFunction.Filtering.Transforms;

namespace WebApiForHikka.SharedFunction.Filtering;

public static partial class Filtering
{
    static Filtering()
    {
        Transforms =
        [
            ..Transforms,
            ..Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                    t is { IsClass: true, IsAbstract: false, IsGenericType: false } &&
                    t.GetConstructors().Any(c => c.GetParameters().Length == 0) &&
                    typeof(IFilteringTransform).IsAssignableFrom(t) && Transforms.All(t2 => t2.GetType() != t))
                .Select(Activator.CreateInstance).OfType<IFilteringTransform>()
        ];
    }

    public static IEnumerable<IFilteringTransform> SortTransforms =>
        Transforms.Where(t => t is not IFilterFilteringTransform);

    public static IEnumerable<IFilteringTransform> FilterTransforms =>
        Transforms.Where(t => t is not ISortFilteringTransform);

    public static string GetReadablePath(this IEnumerable<FilteringItem> items)
    {
        return string.Join(".", items.Select(i => i.ReadableName));
    }

    public static string GetActualPath(this IEnumerable<FilteringItem> items)
    {
        return string.Join(".", items.Select(i => i.ActualName));
    }

    public static FilteringItem GetPropertyInfo(this IEnumerable<FilteringItem> items)
    {
        return items.Last();
    }

    public static IPropertyBase GetProperty(this IEnumerable<FilteringItem> items)
    {
        return items.Last().Property;
    }
}