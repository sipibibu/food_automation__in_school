/*using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> CreateClass(string _class)
        {
            return await _classService.CreateClass(JsonConvert.DeserializeObject<Class>(_class));
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> AddSchoolKidToClass(string classId, string schoolkidId) 
        {
            return await _classService.AddSchoolKidToClass(classId, schoolkidId);
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
        public async Task<BaseResponse<IEnumerable<Class.Entity>>> GetClasses()
        {
            return await _classService.GetClasses();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Class.Entity>> GetClass(string classId)
        {
            return await _classService.GetClass(classId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<Class.Entity>> GetTeachersClass(string teacherId)
        {
            return await _classService.GetTeachersClass(teacherId);
        }
    }
}
*/