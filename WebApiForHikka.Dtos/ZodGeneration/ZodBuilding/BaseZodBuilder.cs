using System.Text;
using WebApiForHikka.Dtos.ZodGeneration.ValueGeneration;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public abstract class BaseZodBuilder<TSelf> : IZodBuilder where TSelf : BaseZodBuilder<TSelf>
{
    private readonly StringBuilder _builder = new();

    protected BaseZodBuilder(string initial)
    {
        _builder.Append(initial);
    }

    public string Build()
    {
        return _builder.ToString();
    }

    IZodBuilder IZodBuilder.Default(object value)
    {
        return Default(value);
    }

    IZodBuilder IZodBuilder.Nullish()
    {
        return Nullish();
    }

    public virtual TSelf Default(object value)
    {
        return AppendChain(TypeScriptValueGenerator.Generate(value));
    }

    public virtual TSelf Nullish()
    {
        return AppendChain("nullish()");
    }

    public override string ToString()
    {
        return Build();
    }

    protected TSelf AppendChain(string chain)
    {
        _builder.Append('.').Append(chain);
        return (TSelf)this;
    }
}