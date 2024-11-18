namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

[Flags]
public enum RegexOptions
{
    None,
    IgnoreCase = 1,
    Multiline = 1 << 1,
    Singleline = 1 << 2,
    Global = 1 << 3
}