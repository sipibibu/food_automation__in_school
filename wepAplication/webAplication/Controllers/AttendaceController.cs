using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
    public class AttendaceController : ControllerBase
    {
        AplicationDbContext db;

        public AttendaceController(AplicationDbContext context) 
        {
            db = context;
        }

/*        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Post(string id, SchoolKidAttendanceType attendance)
        { 
            var schoolKid = db.Person.FirstOrDefault(x=> x.Id == id);

            var atten = db.Attendances.FirstOrDefault(x => x.schoolKidId == id);
            if (schoolKid == null || atten!=null)
            {
                return new BaseResponse<SchoolKidAttendance>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description="govna poesh"
                };
            }

            var schoolKidAttend = new SchoolKidAttendance(id, attendance);
            db.Attendances.Add(schoolKidAttend);
            await db.SaveChangesAsync();
            return new BaseResponse<SchoolKidAttendance>()
            {
                Data = schoolKidAttend,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
         };
        }*/

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Put(string id, SchoolKidAttendanceType attendance)
        {
            var schoolKid = db.Person.FirstOrDefault(x => x.Id == id);
            var atten = db.Attendances.FirstOrDefault(x => x.schoolKidId == id);

            if (schoolKid == null || atten == null)
            {
                return new BaseResponse<SchoolKidAttendance>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            if (attendance == SchoolKidAttendanceType.Uknown)
            {
                return new BaseResponse<SchoolKidAttendance>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "Schoolkid here or not?"
                };
            }

            var schoolKidAttend = new SchoolKidAttendance(id, attendance);
            db.Attendances.Update(schoolKidAttend);
            await db.SaveChangesAsync();
            return new BaseResponse<SchoolKidAttendance>()
            {
                Data = schoolKidAttend,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKidAttendance>> Get(string id)
        {
            var schoolKid = db.Person.FirstOrDefault(x => x.Id == id);
            var atten = db.Attendances.FirstOrDefault(x => x.schoolKidId == id);

            if (schoolKid == null || atten == null)
            {
                return new BaseResponse<SchoolKidAttendance>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            return new BaseResponse<SchoolKidAttendance>()
            {
                Data = atten,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }



    }
}
