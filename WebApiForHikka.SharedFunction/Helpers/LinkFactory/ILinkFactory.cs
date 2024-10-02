using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.SharedFunction.Helpers.LinkFactory;

public interface ILinkFactory
{
    public string GetLinkForDownloadImage(HttpRequest httpRequest, string downloadImageEndpoint,
        string currentEndpointName, string imagePath);
}