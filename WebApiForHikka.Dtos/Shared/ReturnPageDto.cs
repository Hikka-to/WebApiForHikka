namespace WebApiForHikka.Dtos.Shared;

public class ReturnPageDto<T>
{
    public required IReadOnlyCollection<T> Models { get; set; }
    public required int HowManyPages { get; set; }
}