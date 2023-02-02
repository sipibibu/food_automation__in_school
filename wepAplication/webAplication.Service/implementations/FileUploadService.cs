using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.DAL;
using webAplication.Service.Interfaces;

namespace webAplication.Service.implementations
{
    public class FileUploadService : IFileUploadService
    {
        private AplicationDbContext db;

        private readonly ILogger<FileUploadService> _logger;

        public FileUploadService(ILogger<FileUploadService> logger, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
        }
    }
}
