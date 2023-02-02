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
        [Route("[action]")]
        public async Task<BaseResponse<Class>> CreateClass(Class _class)
        {
            return await _classService.CreateClass(_class);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> DeleteClasses(string[] classIds)
        {
            return await _classService.DeleteClasses(classIds);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> UpdateClass(Class _class, string classId)
        {
            return await _classService.UpdateClass(_class, classId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Class>>> GetClasses()
        {
            return await _classService.GetClasses();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> GetClass(string classId)
        {
            return await _classService.GetClass(classId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> GetTeachersClass(string teacherId)
        {
            return await _classService.GetTeachersClass(teacherId);
        }
    }
}
