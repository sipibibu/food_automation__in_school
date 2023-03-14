using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.DAL;
using webAplication.Service.Interfaces;
using webAplication.DAL.models;
using webAplication.Domain;

namespace webAplication.Service.implementations
{
    public class AttendanceService : IAttendanceService
    {
        AplicationDbContext db;
        public AttendanceService(AplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<BaseResponse<SchoolKidAttendanceEntity>> Get(string id)
        {
            var schoolKid = db.Person.FirstOrDefault(x => x.id == id);
            var atten = db.Attendances.FirstOrDefault(x => x.schoolKidId == id);

            if (schoolKid == null || atten == null)
            {
                return new BaseResponse<SchoolKidAttendanceEntity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            return new BaseResponse<SchoolKidAttendanceEntity>()
            {
                Data = atten,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<IEnumerable<SchoolKidAttendanceEntity>>> GetClassAttendance(string classId)
        {
            var _class = db.Classes.FirstOrDefault(x => x.id == classId);

            if (_class == null)
            {
                return new BaseResponse<IEnumerable<SchoolKidAttendanceEntity>>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = $"There is no class weith that id: {classId}"
                };
            
            }
            
            var attendances = new List<SchoolKidAttendanceEntity>();
            foreach (var schoolKidId in _class.schoolKidIds)
            {
                var attendance = db.Attendances.FirstOrDefault(x => x.schoolKidId == schoolKidId);
                attendances.Add(attendance);
            }

            return new BaseResponse<IEnumerable<SchoolKidAttendanceEntity>>()
            {
                Data = attendances,
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<SchoolKidAttendanceEntity>> Post(string id, SchoolKidAttendanceType attendance)
        {
            var schoolKid = await db.Person.FirstOrDefaultAsync(x => x.id == id);
            var atten = await db.Attendances.FirstOrDefaultAsync(x => x.schoolKidId == id);

            if (schoolKid == null || atten != null)
            {
                return new BaseResponse<SchoolKidAttendanceEntity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            if (attendance == SchoolKidAttendanceType.Uknown)
            {
                return new BaseResponse<SchoolKidAttendanceEntity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "Schoolkid here or not?"
                };
            }

            var schoolKidAttend = new SchoolKidAttendanceEntity(id, attendance);
            db.Attendances.Add(schoolKidAttend);
            await db.SaveChangesAsync();
            return new BaseResponse<SchoolKidAttendanceEntity>()
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
