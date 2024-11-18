using System.Collections;
using WebApiForHikka.Dtos.ZodGeneration.ValueGeneration;

namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class MapZodBuilder : TypeBaseZodBuilder<IDictionary, MapZodBuilder>
{
    internal MapZodBuilder(IZodBuilder keyType, IZodBuilder valueType)
        : base($"z.map({keyType.Build()}, {valueType.Build()})")
    {
    }

    public MapZodBuilder MinSize(int size)
    {
        return AppendChain($"min({size})");
    }

    public MapZodBuilder MaxSize(int size)
    {
        return AppendChain($"max({size})");
    }

    public MapZodBuilder Size(int size)
    {
        return AppendChain($"size({size})");
    }

    public override MapZodBuilder Default(IDictionary value)
    {
        return AppendChain($"default({ToTypeScriptMap(value)}");
    }

    private static string ToTypeScriptMap(IDictionary value)
    {
        return $"new Map([{string.Join(", ", value.Keys.Cast<object>()
            .Select(key => $"[{TypeScriptValueGenerator.Generate(key)}, {TypeScriptValueGenerator.Generate(value[key])}]"))}])";
    }
}