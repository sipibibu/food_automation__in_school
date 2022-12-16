using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.implementations;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;

namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        IAttendanceService _attendance;

        public AttendanceController(IAttendanceService context) 
        {
            _attendance = context;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Post(string id, SchoolKidAttendanceType attendance)
        {
            return await _attendance.Post(id, attendance);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Put(string id, SchoolKidAttendanceType attendance)
        {
           return await _attendance.Put(id,attendance);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> ToDefault()
        {
            return await _attendance.ToDefault();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Get(string id)
        {
            return await _attendance.Get(id);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<SchoolKidAttendance>>> GetClassAttendance(string classId)
        {
            return await _attendance.GetClassAttendance(classId);
        }

    }
}
