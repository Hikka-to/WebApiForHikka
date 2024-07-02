using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApiForHikka.EfPersistence.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> source, string propertyName, string filter)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        foreach (var propName in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, propName);
        }

        if (body.Type != typeof(string))
        {
            body = Expression.Call(body, "ToString", Type.EmptyTypes);
        }

        body = Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike", Type.EmptyTypes,
            Expression.Constant(EF.Functions), body, Expression.Constant($"%{filter}%"));

        var lambda = Expression.Lambda(body, param);

        var queryExpr = Expression.Call(typeof(Queryable), "Where", [typeof(T)], source.Expression, lambda);

        return source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IQueryable<T> FilterMany<T>(this IQueryable<T> source, string collectionPropertyName, string filter)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        foreach (var propName in collectionPropertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, propName);
        }

        if (body.Type == typeof(string) ||
            body.Type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            is not { } enumerableType)
        {
            throw new ArgumentException("Property is not a collection", nameof(collectionPropertyName));
        }

        var enumArgType = enumerableType.GetGenericArguments()[0];
        var anyParam = Expression.Parameter(enumArgType, "i");
        var anyBody = (Expression)anyParam;

        if (anyBody.Type != typeof(string))
        {
            anyBody = Expression.Call(anyBody, "ToString", Type.EmptyTypes);
        }

        anyBody = Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike", Type.EmptyTypes,
            Expression.Constant(EF.Functions), anyBody, Expression.Constant($"%{filter}%"));

        var anyLambda = Expression.Lambda(anyBody, anyParam);

        MethodCallExpression finalCall;
        if (body.Type.IsSubclassOf(typeof(IQueryable)))
            finalCall = Expression.Call(typeof(Queryable), "Any", [enumArgType], body, anyLambda);
        else
            finalCall = Expression.Call(typeof(Enumerable), "Any", [enumArgType], body, anyLambda);

        var finalLambda = Expression.Lambda(finalCall, param);

        var queryExpr = Expression.Call(typeof(Queryable), "Where", [typeof(T)], source.Expression, finalLambda);

        return source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IQueryable<T> FilterMany<T>(this IQueryable<T> source, string collectionPropertyName, string propertyName, string filter)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        foreach (var propName in collectionPropertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, propName);
        }

        if (body.Type == typeof(string) ||
            body.Type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            is not { } enumerableType)
        {
            throw new ArgumentException("Property is not a collection", nameof(collectionPropertyName));
        }

        var enumArgType = enumerableType.GetGenericArguments()[0];
        var anyParam = Expression.Parameter(enumArgType, "i");
        var anyBody = (Expression)anyParam;

        foreach (var propName in propertyName.Split('.'))
        {
            anyBody = Expression.PropertyOrField(anyBody, propName);
        }

        if (anyBody.Type != typeof(string))
        {
            anyBody = Expression.Call(body, "ToString", Type.EmptyTypes);
        }

        anyBody = Expression.Call(typeof(NpgsqlDbFunctionsExtensions), "ILike", Type.EmptyTypes,
            Expression.Constant(EF.Functions), anyBody, Expression.Constant($"%{filter}%"));

        var anyLambda = Expression.Lambda(anyBody, anyParam);

        MethodCallExpression finalCall;
        if (body.Type.IsSubclassOf(typeof(IQueryable)))
            finalCall = Expression.Call(typeof(Queryable), "Any", [enumArgType], body, anyLambda);
        else
            finalCall = Expression.Call(typeof(Enumerable), "Any", [enumArgType], body, anyLambda);

        var finalLambda = Expression.Lambda(finalCall, param);

        var queryExpr = Expression.Call(typeof(Queryable), "Where", [typeof(T)], source.Expression, finalLambda);

        return source.Provider.CreateQuery<T>(queryExpr);
    }

    public static IQueryable<T> Sort<T>(this IQueryable<T> source, string propertyName, bool isAscending)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var body = (Expression)param;
        foreach (var propName in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, propName);
        }

        if (body.Type != typeof(string) &&
            body.Type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            is { } enumerableType)
        {
            body = Expression.Call(typeof(Enumerable), "Count", [enumerableType.GetGenericArguments()[0]], body);
        }

        var lambda = Expression.Lambda(body, param);

        var queryExpr = isAscending
            ? Expression.Call(typeof(Queryable), "OrderBy", [typeof(T), body.Type], source.Expression, lambda)
            : Expression.Call(typeof(Queryable), "OrderByDescending", [typeof(T), body.Type], source.Expression, lambda);

        return source.Provider.CreateQuery<T>(queryExpr);
    }
}
