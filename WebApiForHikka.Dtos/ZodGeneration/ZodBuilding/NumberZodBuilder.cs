using System.ComponentModel;
using System.Numerics;
using WebApiForHikka.SharedFunction.Extensions;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class NumberZodBuilder : BaseZodBuilder<NumberZodBuilder>
{
    internal NumberZodBuilder() : base("z.number()")
    {
    }

    public NumberZodBuilder Min(object value)
    {
        return AppendChain($"min({value})");
    }

    public NumberZodBuilder Max(object value)
    {
        return AppendChain($"max({value})");
    }

    public NumberZodBuilder Int()
    {
        return AppendChain("int()");
    }

    public NumberZodBuilder Positive()
    {
        return AppendChain("positive()");
    }

    public NumberZodBuilder Negative()
    {
        return AppendChain("negative()");
    }

    public NumberZodBuilder NonNegative()
    {
        return AppendChain("nonnegative()");
    }

    public NumberZodBuilder NonPositive()
    {
        return AppendChain("nonpositive()");
    }

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    [Obsolete("Use Default(T value) instead", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override NumberZodBuilder Default(object value)
    {
        if (value.GetType().GenericIsSubclassOf(typeof(INumberBase<>)))
            return AppendChain($"default({value})");

        throw new ArgumentException("Value must be a number", nameof(value));
    }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    public NumberZodBuilder Default<T>(T value) where T : INumberBase<T>
    {
        return AppendChain($"default({value})");
    }
}