using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Service.implementations
{
    public class FileUploadService : IFileUploadService
    {
        private AplicationDbContext db;

        private readonly ILogger<FileUploadService> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public FileUploadService(ILogger<FileUploadService> logger, IWebHostEnvironment appEnvironment, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        public string AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "..\\webAplication\\Files\\" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }

                FileModel fileModel = new FileModel(uploadedFile, path);
                db.Files.Add(fileModel.ToEntity());
                db.SaveChanges();
                return fileModel.ToEntity().Id;
            }

            return null;
        }

        public Byte[] GetFile(string fileId)
        {
            var file = db.Files.FirstOrDefault(f => f.Id == fileId);
            Byte[] bytes = System.IO.File.ReadAllBytes(file.Path);
            return bytes;
        }
    }
}
