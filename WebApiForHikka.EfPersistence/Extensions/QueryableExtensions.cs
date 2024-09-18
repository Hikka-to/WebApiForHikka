using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using WebApiForHikka.Domain;

namespace WebApiForHikka.EfPersistence.Extensions;

internal static class QueryableExtensions
{
    private static readonly ReadOnlyDictionary<FilterType, Func<Expression, string, Expression>>
        Filters =
            new Dictionary<FilterType, Func<Expression, string, Expression>>
            {
                [FilterType.Strict] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    return Expression.Equal(body, Expression.Constant(filter));
                },
                [FilterType.Like] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    return Expression.Call(typeof(DbFunctionsExtensions), "Like", Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body,
                        Expression.Constant(filter));
                },
                [FilterType.InsensitiveLike] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    return Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike",
                        Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body,
                        Expression.Constant(filter));
                },
                [FilterType.Contains] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    filter = filter
                        .Replace(@"\", @"\\")
                        .Replace("%", @"\%")
                        .Replace("_", @"\_")
                        .Replace("[", @"\[");
                    return Expression.Call(typeof(DbFunctionsExtensions), "Like", Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body, Expression.Constant($"%{filter}%"),
                        Expression.Constant("\\"));
                },
                [FilterType.InsensitiveContains] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    filter = filter
                        .Replace(@"\", @"\\")
                        .Replace("%", @"\%")
                        .Replace("_", @"\_")
                        .Replace("[", @"\[");
                    return Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike",
                        Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body, Expression.Constant($"%{filter}%"),
                        Expression.Constant("\\"));
                },
                [FilterType.StartsWith] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    return Expression.Call(body, "StartsWith", Type.EmptyTypes,
                        Expression.Constant(filter));
                },
                [FilterType.EndsWith] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    return Expression.Call(body, "EndsWith", Type.EmptyTypes,
                        Expression.Constant(filter));
                },
                [FilterType.InsensitiveStartsWith] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    filter = filter
                        .Replace(@"\", @"\\")
                        .Replace("%", @"\%")
                        .Replace("_", @"\_")
                        .Replace("[", @"\[");
                    return Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike",
                        Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body, Expression.Constant($"{filter}%"),
                        Expression.Constant("\\"));
                },
                [FilterType.InsensitiveEndsWith] = (body, filter) =>
                {
                    if (body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    filter = filter
                        .Replace(@"\", @"\\")
                        .Replace("%", @"\%")
                        .Replace("_", @"\_")
                        .Replace("[", @"\[");
                    return Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike",
                        Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body, Expression.Constant($"%{filter}"),
                        Expression.Constant("\\"));
                },
                [FilterType.Bigger] = (body, filter) =>
                {
                    var parsedFilter = ToType(body.Type, filter);
                    var compare = Expression.Call(body, "CompareTo", Type.EmptyTypes,
                        Expression.Constant(parsedFilter));
                    return Expression.GreaterThan(compare, Expression.Constant(0));
                },
                [FilterType.Smaller] = (body, filter) =>
                {
                    var parsedFilter = ToType(body.Type, filter);
                    var compare = Expression.Call(body, "CompareTo", Type.EmptyTypes,
                        Expression.Constant(parsedFilter));
                    return Expression.LessThan(compare, Expression.Constant(0));
                },
                [FilterType.BiggerOrEqual] = (body, filter) =>
                {
                    var parsedFilter = ToType(body.Type, filter);
                    var compare = Expression.Call(body, "CompareTo", Type.EmptyTypes,
                        Expression.Constant(parsedFilter));
                    return Expression.GreaterThanOrEqual(compare, Expression.Constant(0));
                },
                [FilterType.SmallerOrEqual] = (body, filter) =>
                {
                    var parsedFilter = ToType(body.Type, filter);
                    var compare = Expression.Call(body, "CompareTo", Type.EmptyTypes,
                        Expression.Constant(parsedFilter));
                    return Expression.LessThanOrEqual(compare, Expression.Constant(0));
                },
                [FilterType.WebSearch] = (body, filter) =>
                {
                    if (body.Type != typeof(NpgsqlTsVector) && body.Type != typeof(string))
                        body = Expression.Call(body, "ToString", Type.EmptyTypes);
                    if (body.Type != typeof(NpgsqlTsVector))
                        body = Expression.Call(typeof(NpgsqlFullTextSearchDbFunctionsExtensions),
                            "ToTsVector", Type.EmptyTypes, Expression.Constant(EF.Functions), body);

                    var query =
                        Expression.Call(typeof(NpgsqlFullTextSearchDbFunctionsExtensions),
                            "WebSearchToTsQuery", Type.EmptyTypes,
                            Expression.Constant(EF.Functions),
                            Expression.Constant(filter));
                    return Expression.Call(typeof(NpgsqlFullTextSearchLinqExtensions), "Matches",
                        Type.EmptyTypes, body, query);
                }
            }.AsReadOnly();

    private static object ToType(Type type, string filter)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;

        if (type == typeof(string))
            return filter;

        if (type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m => m.Name == "Parse" && m.GetParameters().Length == 1 &&
                                     m.GetParameters()[0].ParameterType == typeof(string)) is
            { } parseMethod)
            return parseMethod.Invoke(null, [filter])!;

        if (type.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length == 1 &&
                                     c.GetParameters()[0].ParameterType == typeof(string)) is
            { } constructor)
            return constructor.Invoke([filter]);

        throw new ArgumentException("Type not supported", nameof(type));
    }

    private static bool IsNullableType(this Type type, string propertyOrField)
    {
        var nullabilityContext = new NullabilityInfoContext();
        if (type.GetProperty(propertyOrField) is { } propertyInfo)
        {
            var nullabilityInfo = nullabilityContext.Create(propertyInfo);
            return nullabilityInfo.WriteState is NullabilityState.Nullable;
        }

        if (type.GetField(propertyOrField) is not { } fieldInfo) return false;
        {
            var nullabilityInfo = nullabilityContext.Create(fieldInfo);
            return nullabilityInfo.WriteState is NullabilityState.Nullable;
        }
    }

    private static string GetParameterName(int index)
    {
        return $"p{index}";
    }

    private static Expression NullConditional(this Expression expression, Expression body,
        string propertyOrField)
    {
        var property = Expression.PropertyOrField(expression, propertyOrField);

        return expression.Type.IsNullableType(propertyOrField)
            ? Expression.Condition(Expression.NotEqual(property, Expression.Constant(null)),
                body,
                Expression.Default(body.Type))
            : body;
    }

    private static Expression GetFilterBody(Expression body, string[] properties, Filter filter,
        int paramIndex = 0)
    {
        if (body.Type != typeof(string) && body.Type.GetInterfaces()
                    .FirstOrDefault(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is
                { } enumerableType)
        {
            var enumArgType = enumerableType.GetGenericArguments()[0];
            var param = Expression.Parameter(enumArgType, GetParameterName(paramIndex + 1));
            Expression anyBody = param;
            anyBody = GetFilterBody(anyBody, properties, filter, paramIndex + 1);

            var anyLambda = Expression.Lambda(anyBody, param);
            var anyCall = Expression.Call(
                typeof(IQueryable).IsAssignableFrom(body.Type)
                    ? typeof(Queryable)
                    : typeof(Enumerable), "Any",
                [enumArgType], body, anyLambda);
            return anyCall;
        }

        if (properties.Length != 0)
        {
            var property = Expression.PropertyOrField(body, properties[0]);

            if (Nullable.GetUnderlyingType(property.Type) is not null)
                property = Expression.Property(property, "Value");

            return body.NullConditional(
                GetFilterBody(property,
                    properties[1..],
                    filter),
                properties[0]);
        }

        var filterType = filter.FilterType is
            FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch
            ? FilterType.WebSearch
            : filter.FilterType;

        body = Filters[filterType](body, filter.SearchTerm);

        if (filter.Negate)
            body = Expression.Not(body);

        return body;
    }

    private static Expression GetSortBody(Expression body, string[] properties, int paramIndex = 0)
    {
        if (body.Type != typeof(string) && body.Type.GetInterfaces()
                    .FirstOrDefault(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is
                { } enumerableType)
        {
            var enumArgType = enumerableType.GetGenericArguments()[0];
            if (properties.Length == 0)
                return Expression.Call(
                    typeof(IQueryable).IsAssignableFrom(body.Type)
                        ? typeof(Queryable)
                        : typeof(Enumerable),
                    "Count", [enumArgType], body);

            var param = Expression.Parameter(enumArgType, GetParameterName(paramIndex + 1));
            Expression sumBody = param;
            sumBody = GetSortBody(sumBody, properties);
            var sumLambda = Expression.Lambda(sumBody, param);
            var sumCall =
                Expression.Call(typeof(Enumerable), "Sum", [enumArgType], body, sumLambda);
            return sumCall;
        }

        if (properties.Length == 0)
            return typeof(bool).IsAssignableFrom(body.Type)
                ? Expression.Condition(body, Expression.Constant(1), Expression.Constant(0))
                : body;

        var property = Expression.PropertyOrField(body, properties[0]);

        if (Nullable.GetUnderlyingType(property.Type) is not null)
            property = Expression.Property(property, "Value");

        return body.NullConditional(
            GetSortBody(property, properties[1..]),
            properties[0]);
    }

    private static Expression GetRankSortBody(Expression body, string[] properties, string filter,
        int paramIndex = 0)
    {
        if (body.Type != typeof(string) && body.Type.GetInterfaces()
                    .FirstOrDefault(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is
                { } enumerableType)
        {
            var enumArgType = enumerableType.GetGenericArguments()[0];
            var param = Expression.Parameter(enumArgType, GetParameterName(paramIndex + 1));
            Expression anyBody = param;
            anyBody = GetRankSortBody(anyBody, properties, filter, paramIndex + 1);

            var anyLambda = Expression.Lambda(anyBody, param);
            var anyCall = Expression.Call(
                typeof(IQueryable).IsAssignableFrom(body.Type)
                    ? typeof(Queryable)
                    : typeof(Enumerable), "Any",
                [enumArgType], body, anyLambda);
            return anyCall;
        }

        if (properties.Length != 0)
        {
            var property = Expression.PropertyOrField(body, properties[0]);

            if (Nullable.GetUnderlyingType(property.Type) is not null)
                property = Expression.Property(property, "Value");

            return body.NullConditional(
                GetRankSortBody(property,
                    properties[1..],
                    filter),
                properties[0]);
        }

        if (body.Type != typeof(NpgsqlTsVector) && body.Type != typeof(string))
            body = Expression.Call(body, "ToString", Type.EmptyTypes);
        if (body.Type != typeof(NpgsqlTsVector))
            body = Expression.Call(typeof(NpgsqlFullTextSearchDbFunctionsExtensions),
                "ToTsVector", Type.EmptyTypes, Expression.Constant(EF.Functions), body);

        var query =
            Expression.Call(typeof(NpgsqlFullTextSearchDbFunctionsExtensions),
                "WebSearchToTsQuery", Type.EmptyTypes,
                Expression.Constant(EF.Functions),
                Expression.Constant(filter));
        return Expression.Call(typeof(NpgsqlFullTextSearchLinqExtensions), "Rank",
            Type.EmptyTypes, body, query);
    }

    private static IOrderedQueryable<T> RankSort<T>(this IQueryable<T> source, string propertyName,
        string filter,
        bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        Expression body = param;
        body = GetRankSortBody(body, propertyName.Split('.'), filter);

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "OrderBy", [typeof(T), body.Type],
                source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "OrderByDescending", [typeof(T), body.Type],
                source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }

    private static IOrderedQueryable<T> ThenRankSort<T>(this IOrderedQueryable<T> source,
        string propertyName, string filter,
        bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        Expression body = param;
        body = GetRankSortBody(body, propertyName.Split('.'), filter);

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "ThenBy", [typeof(T), body.Type],
                source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "ThenByDescending", [typeof(T), body.Type],
                source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> source, Filter filter)
    {
        var param = Expression.Parameter(typeof(T), GetParameterName(0));
        Expression body = param;
        body = GetFilterBody(body, filter.Column.Split('.'), filter);

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        return filter.FilterType switch
        {
            FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch when
                typeof(IOrderedQueryable<T>).IsAssignableFrom(source.Expression.Type) =>
                ((IOrderedQueryable<T>)source)
                .ThenRankSort(filter.Column, filter.SearchTerm,
                    filter.FilterType == FilterType.OrderedWebSearch)
                .Where(lambda),
            FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch => source
                .Where(lambda)
                .RankSort(filter.Column, filter.SearchTerm,
                    filter.FilterType == FilterType.OrderedWebSearch),
            _ => source.Where(lambda)
        };
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> source, IEnumerable<Filter> filters)
    {
        filters = filters.ToArray();

        var param = Expression.Parameter(typeof(T), GetParameterName(0));
        Expression body = param;
        body = filters.Select(f => GetFilterBody(body, f.Column.Split('.'), f))
            .Aggregate(Expression.Or);

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        if (!typeof(IOrderedQueryable<T>).IsAssignableFrom(source.Expression.Type))
            return filters.Aggregate(source.Where(lambda), (current, filter) =>
                filter.FilterType switch
                {
                    FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch when
                        typeof(IOrderedQueryable<T>).IsAssignableFrom(current.Expression.Type) =>
                        ((IOrderedQueryable<T>)current).ThenRankSort(
                            filter.Column, filter.SearchTerm,
                            filter.FilterType == FilterType.OrderedWebSearch)
                        .Where(lambda),
                    FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch =>
                        current.RankSort(filter.Column, filter.SearchTerm,
                            filter.FilterType == FilterType.OrderedWebSearch),
                    _ => current
                });
        return filters.Aggregate((IOrderedQueryable<T>)source, (current, filter) =>
            filter.FilterType switch
            {
                FilterType.OrderedWebSearch or FilterType.DescendingOrderedWebSearch =>
                    current.ThenRankSort(filter.Column, filter.SearchTerm,
                        filter.FilterType == FilterType.OrderedWebSearch),
                _ => current
            }).Where(lambda);
    }

    public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> source, string propertyName,
        bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        Expression body = param;
        body = GetSortBody(body, propertyName.Split('.'));

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "OrderBy", [typeof(T), body.Type],
                source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "OrderByDescending", [typeof(T), body.Type],
                source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IOrderedQueryable<T> ThenSort<T>(this IOrderedQueryable<T> source,
        string propertyName,
        bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        Expression body = param;
        body = GetSortBody(body, propertyName.Split('.'));

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "ThenBy", [typeof(T), body.Type],
                source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "ThenByDescending", [typeof(T), body.Type],
                source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }
}