using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace WebApiForHikka.EfPersistence.Extensions;

public static class QueryableExtensions
{
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

    private static Expression NullConditional(this Expression expression, string propertyOrField)
    {
        var property = Expression.PropertyOrField(expression, propertyOrField);
        
        return expression.Type.IsNullableType(propertyOrField) 
            ? Expression.Condition(Expression.NotEqual(property, Expression.Constant(null)),
                property,
                Expression.Default(property.Type))
            : property;
    }

    private static Expression GetFilterBody(Expression body, string[] properties, string filter, bool isStrict = false,
        int paramIndex = 0)
    {
        while (true)
        {
            if (body.Type != typeof(string) && body.Type.GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is
                    { } enumerableType)
            {
                var enumArgType = enumerableType.GetGenericArguments()[0];
                var param = Expression.Parameter(enumArgType, GetParameterName(paramIndex + 1));
                var anyBody = (Expression)param;
                anyBody = GetFilterBody(anyBody, properties, filter, isStrict, paramIndex + 1);

                var anyLambda = Expression.Lambda(anyBody, param);
                var anyCall = Expression.Call(
                    typeof(IQueryable).IsAssignableFrom(body.Type) ? typeof(Queryable) : typeof(Enumerable), "Any",
                    [enumArgType], body, anyLambda);
                return anyCall;
            }

            if (properties.Length == 0)
            {
                if (body.Type != typeof(string)) body = Expression.Call(body, "ToString", Type.EmptyTypes);

                return isStrict
                    ? Expression.Equal(body, Expression.Constant(filter))
                    : Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike", Type.EmptyTypes,
                        Expression.Constant(EF.Functions), body, Expression.Constant($"%{filter}%"));
            }

            body = body.NullConditional(properties[0]);
            properties = properties[1..];
        }
    }

    private static Expression GetSortBody(Expression body, string[] properties, int paramIndex = 0)
    {
        while (true)
        {
            if (body.Type != typeof(string) && body.Type.GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is
                    { } enumerableType)
            {
                var enumArgType = enumerableType.GetGenericArguments()[0];
                if (properties.Length == 0)
                    return Expression.Call(
                        typeof(IQueryable).IsAssignableFrom(body.Type) ? typeof(Queryable) : typeof(Enumerable),
                        "Count", [enumArgType], body);

                var param = Expression.Parameter(enumArgType, GetParameterName(paramIndex + 1));
                var sumBody = (Expression)param;
                sumBody = GetSortBody(sumBody, properties);
                var sumLambda = Expression.Lambda(sumBody, param);
                var sumCall = Expression.Call(typeof(Enumerable), "Sum", [enumArgType], body, sumLambda);
                return sumCall;
            }

            if (properties.Length == 0)
                return typeof(bool).IsAssignableFrom(body.Type)
                    ? Expression.Condition(body, Expression.Constant(1), Expression.Constant(0))
                    : body;

            body = NullConditional(body, properties[0]);
            properties = properties[1..];
        }
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> source, string propertyName, string filter,
        bool isStrict = false)
    {
        var param = Expression.Parameter(typeof(T), GetParameterName(0));
        var body = (Expression)param;
        body = GetFilterBody(body, propertyName.Split('.'), filter, isStrict);

        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        return source.Where(lambda);
    }

    public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> source, string propertyName, bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        body = GetSortBody(body, propertyName.Split('.'));

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "OrderBy", [typeof(T), body.Type], source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "OrderByDescending", [typeof(T), body.Type], source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IOrderedQueryable<T> ThenSort<T>(this IOrderedQueryable<T> source, string propertyName,
        bool isAscending = true)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        body = GetSortBody(body, propertyName.Split('.'));

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "ThenBy", [typeof(T), body.Type], source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "ThenByDescending", [typeof(T), body.Type], source.Expression,
                lambda);

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(queryExpr);
    }
}