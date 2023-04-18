using Microsoft.AspNetCore.Mvc;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        AplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;
        private IFileUploadService _fileUploadService;

        public FileUploadController(AplicationDbContext context, IWebHostEnvironment appEnvironment
        ,IFileUploadService fileUploadService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<BaseResponse<string>> AddFile(IFormFile uploadedFile)
        {
            try
            {
                var fileId = _fileUploadService.AddFile(uploadedFile);
                return new BaseResponse<string>
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = fileId
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "File was null"
                };
            }
        }

        [HttpGet]
        public async Task<BaseResponse<string>> GetFile(string fileId)
        {
            try
            {
                var bytes = _fileUploadService.GetFile(fileId);
                return new BaseResponse<string>()
                {
                    StatusCode= Domain.StatusCode.OK,
                    Data = Convert.ToBase64String(bytes)
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = exception.Message 
                };
            }
        }
    }
}
