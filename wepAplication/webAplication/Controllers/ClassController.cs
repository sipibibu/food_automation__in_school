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
        public async Task<BaseResponse<string>> CreateClass(string _class)
        {
            try
            {
                var result = _classService.CreateClass(JsonConvert.DeserializeObject<Class>(_class));
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<string>()
                {
                    Description = exception.Message,
                    StatusCode = Domain.StatusCode.BAD
                };
            }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<string>> AddSchoolKidToClass(string classId, string schoolkidId)
        {
            try
            {
                var _class = _classService.GetClass(classId);
                //todo validation that is real kid :)
                SchoolKid schoolKid = (SchoolKid) _accountService.GetPerson(schoolkidId);
                await _classService.AddSchoolKid(_class, schoolKid);
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(_class),
                    StatusCode = Domain.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<string>()
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
        public BaseResponse<string> UpdateClass(string classJson)
        {
            try
            {
                var _class = JsonConvert.DeserializeObject<Class>(classJson);
                var result = _classService.UpdateClass(_class);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<string>>> GetClasses()
        {
            try
            {
                var classes = _classService.GetClasses();
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = classes.Select(x => JsonConvert.SerializeObject(x))
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<string>>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<string>> GetClass(string classId)
        {
            try
            {
                var _class = _classService.GetClass(classId);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_class)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<string>> GetTeachersClass(string teacherId)
        {
            try
            {
                var _class = _classService.GetTeacherClass(teacherId);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(_class)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
    }
}
