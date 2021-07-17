using AutoMapper.Configuration;
using CleanApplication.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string root;
        private readonly string folderUploads = "/uploads/";
        //private readonly IConfiguration _configuration;
        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            root = _hostEnvironment.WebRootPath + folderUploads;
        }
        public async Task<string> FileUpload(string folder, string fileName, IFormFile ImageFile, string extensionFile)
        {
    
            var baseAddress = root;
            if (string.IsNullOrEmpty(folder))
                baseAddress += $"{folder}/";
            string path = Path.Combine(baseAddress, fileName);
            CreatePath(baseAddress);

            using (var fileStream = new FileStream($"{path}{extensionFile}", FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }



            return $"{folderUploads}{fileName}{extensionFile}";
        }

        public bool RemoveFile(string path, string fileName, string extensionFile)
        {
            string _path = Path.Combine(path, $"{fileName}{extensionFile}");

            //var filePath = Server.MapPath("~/Images/" + filename);
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }

            return true;
        }

        public string ThumbImageUpload(string folder, string fileName, IFormFile ImageFile, string extensionFile, int width = 100, int height = 100)
        {
            var baseAddress = root;
            if (string.IsNullOrEmpty(folder))
                baseAddress += $"{folder}/";
            string path = Path.Combine(baseAddress, $"{fileName}{extensionFile}");
            CreatePath(baseAddress);
            Image image = Image.FromStream(ImageFile.OpenReadStream(), true, true);
            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            newImage.Save(path);
            return $"{folderUploads}{fileName}{extensionFile}";
        }
        private void CreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
    }
}
