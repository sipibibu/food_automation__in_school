using Microsoft.AspNetCore.Mvc;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        readonly IAttendanceService _attendance;

        public AttendanceController(IAttendanceService context) 
        {
            _attendance = context;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance.Entity>> Post(SchoolKidAttendance.Entity entity)
        {
            return await _attendance.Post(entity);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance.Entity>> Put(string id, SchoolKidAttendance.SchoolKidAttendanceType attendance)
        {
            return await _attendance.Put(id, attendance);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance.Entity>> ToDefault()
        {
            return await _attendance.ToDefault();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance.Entity>> Get(string id)
        {
            return await _attendance.Get(id);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<SchoolKidAttendance.Entity>>> GetClassAttendance(string classId)
        {
            return await _attendance.GetClassAttendance(classId);
        }

    }
}
