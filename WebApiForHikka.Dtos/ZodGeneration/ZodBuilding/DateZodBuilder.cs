namespace WebApiForHikka.Dtos.ZodGeneration.ZodBuilding;

public class DateZodBuilder : TypeBaseZodBuilder<DateTime, DateZodBuilder>
{
    internal DateZodBuilder() : base("z.date()")
    {
    }

    public DateZodBuilder Min(DateTime date)
    {
        return AppendChain($"min(new Date(\"{date:O}\"))");
    }

    public DateZodBuilder Max(DateTime date)
    {
        return AppendChain($"max(new Date(\"{date:O}\"))");
    }
}