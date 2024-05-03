namespace WebApiForHikka.WebApi.Dto.Authorization;
public record JwtTokenContentDto
{
    public required string? Email;
    public required string? Role;
    public required string? Id;
}
