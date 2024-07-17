using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using System.Drawing;
using Image = SixLabors.ImageSharp.Image;

namespace WebApiForHikka.WebApi.Helper.FileHelper;

public class FileHelper : IFileHelper
{
    public void DeleteFile(string[] path, string fileName)
    {
        try
        {

        File.Delete(Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path)) + fileName);
        }
        catch (DirectoryNotFoundException)
        {
        }
    }

    public void DeleteFile(string path) 
    {
        try
        {

            File.Delete(path);
        }
        catch (DirectoryNotFoundException)
        {

        }
    }

    public byte[] GetFile(string path)
    {
        return File.ReadAllBytes(path);
    }
    public byte[] GetFile(string[] path, string fileName)
    {
        var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));
        var filePath = Path.Combine(targetDirectory, fileName);

        return File.ReadAllBytes(filePath);
    }

    public void OverrideFileImage(IFormFile file, string path)
    {
        using var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
        var encoderOptions = new WebpEncoder { Quality = 80 }; // Adjust quality as needed
        image.Save(path, encoderOptions);
    }


    public string UploadFileImage(IFormFile file, string[] path)
    {
        // GetAsync the extension of the uploaded file
        var originalExtension = Path.GetExtension(file.FileName);

        // Define the target directory where images will be saved
        var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));

        // Ensure the target directory exists
        Directory.CreateDirectory(targetDirectory);

        // Generate a unique filename using a GUID
        Guid fileNameWithoutExtension = Guid.NewGuid();
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

    public string UploadFileImage(IFormFile file, string[] path, string fileName)
    {
        // GetAsync the extension of the uploaded file
        var originalExtension = Path.GetExtension(file.FileName);

        // Define the target directory where images will be saved
        var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), string.Join("\\", path));

        // Ensure the target directory exists
        Directory.CreateDirectory(targetDirectory);

        var webPFileName = $"{fileName}.webp";

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

    public (int height, int width) GetHeightAndWidthOfImage(IFormFile file) 
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var image = new Bitmap(memoryStream);

        // Get the width and height of the image
        int width = image.Width;
        int height = image.Height;
        
        return (height, width);

    }

   }