using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.LinkFactory;

public interface ILinkFactory
{
    public string GetLinkForDowloadImage(HttpRequest httpRequest, string dowloadImageEndpoint, string currectEnpointName, string imagePath);
}
