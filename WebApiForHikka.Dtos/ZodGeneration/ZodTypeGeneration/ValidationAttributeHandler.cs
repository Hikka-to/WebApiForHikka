using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodTypeGeneration;

internal static class ValidationAttributeHandler
{
    public static void ApplyValidationAttributes(IZodBuilder zodBuilder, MemberInfo member,
        Type type)
    {
        switch (zodBuilder)
        {
            case StringZodBuilder stringZodBuilder:
                ApplyStringValidations(stringZodBuilder, member);
                break;
            case NumberZodBuilder numberZodBuilder:
                ApplyNumberValidations(numberZodBuilder, member, type);
                break;
        }
    }

    private static void ApplyStringValidations(StringZodBuilder stringZodBuilder, MemberInfo member)
    {
        if (GetCustomAttribute<StringLengthAttribute>(member) is { } stringLength)
            stringZodBuilder.Max(stringLength.MaximumLength);

        if (GetCustomAttribute<EmailAddressAttribute>(member) is not null)
            stringZodBuilder.Email();

        if (GetCustomAttribute<UrlAttribute>(member) is not null)
            stringZodBuilder.Url();

        if (GetCustomAttribute<RequiredAttribute>(member) is { AllowEmptyStrings: false })
            stringZodBuilder.Regex("\\S");

        if (GetCustomAttribute<MinLengthAttribute>(member) is { } minLength)
            stringZodBuilder.Min(minLength.Length);

        if (GetCustomAttribute<MaxLengthAttribute>(member) is { } maxLength)
            stringZodBuilder.Max(maxLength.Length);

        if (GetCustomAttribute<RegularExpressionAttribute>(member) is { } regex)
            stringZodBuilder.Regex(regex.Pattern);
    }

    private static void ApplyNumberValidations(NumberZodBuilder numberZodBuilder, MemberInfo member,
        Type type)
    {
        if (GetCustomAttribute<RangeAttribute>(member) is { } range)
            ApplyRangeAttribute(numberZodBuilder, range, type);
    }

    private static TAttribute? GetCustomAttribute<TAttribute>(MemberInfo member)
        where TAttribute : Attribute
    {
        while (true)
        {
            if (member.GetCustomAttribute<TAttribute>() is { } attribute)
                return attribute;

            if (member.DeclaringType?.GetCustomAttribute<ModelMetadataTypeAttribute>() is
                { } modelMetadataType)
            {
                var metadataType = modelMetadataType.MetadataType;
                var metadataProperty = metadataType.GetProperty(member.Name);
                if (metadataProperty is not null)
                {
                    member = metadataProperty;
                    continue;
                }
            }

            for (var baseType = member is PropertyInfo property
                     ? property.PropertyType
                     : ((FieldInfo)member).FieldType;
                 baseType != null;
                 baseType = baseType.BaseType)
            {
                var baseProperty = baseType.GetProperty(member.Name);
                if (baseProperty?.GetCustomAttribute<TAttribute>() is { } baseAttribute)
                    return baseAttribute;
            }

            return null;
        }
    }

    private static void ApplyRangeAttribute(NumberZodBuilder zodBuilder, RangeAttribute range,
        Type type)
    {
        var min = range.Minimum;
        var max = range.Maximum;

        if (IsInfinite(min)) min = GetTypeMin(type);
        if (IsInfinite(max)) max = GetTypeMax(type);

        if (min != null) zodBuilder.Min(min);
        if (max != null) zodBuilder.Max(max);
    }

    private static bool IsInfinite(object? value)
    {
        return value != null && (
            double.NegativeInfinity.Equals(value) ||
            float.NegativeInfinity.Equals(value) ||
            double.PositiveInfinity.Equals(value) ||
            float.PositiveInfinity.Equals(value));
    }

    private static object? GetTypeMin(Type type)
    {
        try
        {
            var minValue = typeof(ZodTypeGenerator)
                .GetMethod(nameof(MinValueGetter), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(type)
                .Invoke(null, null);

            return !IsInfinite(minValue) ? minValue : null;
        }
        catch
        {
            return null;
        }
    }

    private static object? GetTypeMax(Type type)
    {
        try
        {
            var maxValue = typeof(ZodTypeGenerator)
                .GetMethod(nameof(MaxValueGetter), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(type)
                .Invoke(null, null);

            return !IsInfinite(maxValue) ? maxValue : null;
        }
        catch
        {
            return null;
        }
    }

    private static object MinValueGetter<T>() where T : IMinMaxValue<T>
    {
        return T.MinValue;
    }

    private static object MaxValueGetter<T>() where T : IMinMaxValue<T>
    {
        return T.MaxValue;
    }
}