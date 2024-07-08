namespace WebApiForHikka.WebApi.Helper.FileHelper;

public interface IFileHelper
{
    public string UploadFile(IFormFile file, string[] path);
}