using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Service.Interfaces;

namespace webAplication.Service.implementations
{
    public class AttendanceService : IAttendanceService
    {
        AplicationDbContext db;
        public AttendanceService(AplicationDbContext db)
        {
            this.db = db;
        }

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

        public async Task<BaseResponse<SchoolKidAttendance>> Post(string id, SchoolKidAttendanceType attendance)
        {
            var schoolKid = await db.Person.FirstOrDefaultAsync(x => x.Id == id);
            var atten = await db.Attendances.FirstOrDefaultAsync(x => x.schoolKidId == id);

            if (schoolKid == null || atten != null)
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
            db.Attendances.Add(schoolKidAttend);
            await db.SaveChangesAsync();
            return new BaseResponse<SchoolKidAttendance>()
            {
                Data = schoolKidAttend,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<SchoolKidAttendance>> Put(string id, SchoolKidAttendanceType attendance)
        {
            var schoolKid = await db.Person.FirstOrDefaultAsync(x => x.Id == id);
            var atten = await db.Attendances.FirstOrDefaultAsync(x => x.schoolKidId == id);

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

            atten.schoolKidAttendanceType= attendance;
            db.Attendances.Update(atten);
            await db.SaveChangesAsync();
            return new BaseResponse<SchoolKidAttendance>()
            {
                Data = atten,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<SchoolKidAttendance>> ToDefault()
        {
            foreach (var i in db.Attendances)
            {
                i.schoolKidAttendanceType = SchoolKidAttendanceType.Uknown;
                db.Attendances.Update(i);
            }
            await db.SaveChangesAsync();

            return new BaseResponse<SchoolKidAttendance>()
            {
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }
    }
}
