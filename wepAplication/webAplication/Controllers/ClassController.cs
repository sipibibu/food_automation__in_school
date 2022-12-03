using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public BaseResponse<Class> CreateClass(Class _class)
        {
            return _classService.CreateClass(_class);
        }
    }
}
