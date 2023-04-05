using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        readonly IClassService _classService;
        readonly IAccountService _accountService;
        public ClassController(IClassService classService, IAccountService accountService)
        {
            _classService = classService;
            _accountService = accountService;
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
            try
            {
                var _class = _classService.GetClass(classId);
                //todo validation that is real kid :)
                SchoolKid schoolKid = (SchoolKid) _accountService.GetPerson(schoolkidId);
                await _classService.AddSchoolKid(_class, schoolKid);
                return new BaseResponse<Class>()
                {
                    Data = _class,
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<Class>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<BaseResponse<Class>> DeleteClasses(string[] classIds)
        {
            return await _classService.DeleteClasses(classIds);
        }

        [HttpPut]
        [Route("[action]")]
        public BaseResponse<Class> UpdateClass(string classJson)
        {
            try
            {
                var _class = JsonConvert.DeserializeObject<Class>(classJson);
                var result = _classService.UpdateClass(_class);
                return new BaseResponse<Class>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = result
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<Class>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        // [HttpGet]
        // [Route("[action]")]
        // public async Task<BaseResponse<IEnumerable<Class.Entity>>> GetClasses()
        // {
        //     return await _classService.GetClasses();
        // }
        //
        // [HttpGet]
        // [Route("[action]")]
        // public async Task<BaseResponse<Class.Entity>> GetClass(string classId)
        // {
        //     return await _classService.GetClass(classId);
        // }
        //
        // [HttpGet]
        // [Route("[action]")]
        // public async Task<BaseResponse<Class.Entity>> GetTeachersClass(string teacherId)
        // {
        //     return await _classService.GetTeachersClass(teacherId);
        // }
    }
}
