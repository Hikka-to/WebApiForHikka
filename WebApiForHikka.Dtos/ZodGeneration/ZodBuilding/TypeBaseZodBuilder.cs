using System.ComponentModel;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public abstract class TypeBaseZodBuilder<T, TSelf>(string initial) : BaseZodBuilder<TSelf>(initial)
    where TSelf : TypeBaseZodBuilder<T, TSelf>
{
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    [Obsolete("Use Default(T value) instead", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override TSelf Default(object value)

    {
        return Default((T)value);
    }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    public virtual TSelf Default(T value)
    {
        return AppendChain($"default({value})");
    }
}