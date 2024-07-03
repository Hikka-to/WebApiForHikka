using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace WebApiForHikka.WebApi.Helper.FileHelper;

public class FileHelper : IFileHelper
{
    public string UploadFile(IFormFile file, string[] path)
    {
        // Get the extension of the uploaded file
        var originalExtension = Path.GetExtension(file.FileName);

        // Define the target directory where images will be saved
        var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));

        // Ensure the target directory exists
        Directory.CreateDirectory(targetDirectory);

        // Generate a unique filename using a GUID
        var fileNameWithoutExtension = Guid.NewGuid().ToString();
        var webPFileName = $"{fileNameWithoutExtension}.webp";

        // Full path to save the converted image
        var filePath = Path.Combine(targetDirectory, webPFileName);

        // Convert and save the image if it's not already in WebP format
        if (originalExtension != ".webp")
        {
            using var image = Image.Load(file.OpenReadStream());
            var encoderOptions = new WebpEncoder { Quality = 80 }; // Adjust quality as needed
            image.Save(filePath, encoderOptions);
        }
        else
        {
            // If the file is already in WebP format, just copy it to the target directory
            using var fileStream = File.Create(filePath);
            file.CopyTo(fileStream);
        }

        return filePath;
    }
}
