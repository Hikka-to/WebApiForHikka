﻿using Microsoft.AspNetCore.Http;

namespace WebApiForHikka.WebApi.Helper.FileHelper;

public interface IFileHelper
{
    public string UploadFileImage(IFormFile file, string[] path, string fileName);
    public string UploadFileImage(IFormFile file, string[] path);

    public void OverrideFileImage(IFormFile file, string path);
    public void DeleteFile(string[] path, string fileName);
    public void DeleteFile(string path);
    public byte[] GetFile(string path);
    public byte[] GetFile(string[] path, string fileName);
}