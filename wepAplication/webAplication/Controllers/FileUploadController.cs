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

        public FileUploadController(AplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = @"D:\projects\food_automation__in_school\wepAplication\webAplication\Files\" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<BaseResponse<string>> GetFile(string fileId)
        {
            try
            {
                var file = _context.Files.FirstOrDefault(f => f.Id == fileId);
                Byte[] bytes = System.IO.File.ReadAllBytes(file.Path);

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
                    StatusCode= Domain.StatusCode.BAD
                };
            }
        }
    }
}
