using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.ColorHelper;

public interface IColorHelper
{
    public List<int> GetListOfColorsFromImage(IFormFile file);
}
