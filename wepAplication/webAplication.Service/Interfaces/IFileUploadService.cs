using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace webAplication.Service.Interfaces
{
    public interface IFileUploadService
    {
        string AddFile(IFormFile uploadedFile);
        Byte[] GetFile(string fileId);
    }
}
