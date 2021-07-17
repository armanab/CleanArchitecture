using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IImageService
    {
        Task<string> FileUpload(string folder, string fileName, IFormFile ImageFile, string extensionFile);
        string ThumbImageUpload(string folder, string fileName, IFormFile ImageFile, string extensionFile, int width = 100, int height = 100);
        bool RemoveFile(string path, string fileName, string extensionFile);

    }
}
