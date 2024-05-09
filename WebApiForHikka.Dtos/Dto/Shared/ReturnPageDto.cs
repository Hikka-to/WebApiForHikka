namespace WebApiForHikka.Dtos.Dto.Shared;

public abstract class ReturnPageDto<T>
{

    public required IReadOnlyCollection<T> Models { get; set; }
    public required int HowManyPages { get; set; }
}
