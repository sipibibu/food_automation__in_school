using Microsoft.EntityFrameworkCore;
using webAplication.DAL;
using webAplication.Service.Interfaces;
using webAplication.Domain;
using static webAplication.Domain.SchoolKidAttendance;


namespace webAplication.Service.implementations
{
    public class AttendanceService : IAttendanceService
    {
        AplicationDbContext db;
        public AttendanceService(AplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<BaseResponse<Entity>> Get(string id)
        {
            var schoolKid = db.Person.FirstOrDefault(x => x.Id == id);
            var atten = db.Attendances.FirstOrDefault(x => x.Id == id);

            if (schoolKid == null || atten == null)
            {
                return new BaseResponse<Entity>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            return new BaseResponse<Entity>()
            {
                Data = atten,
                StatusCode = StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<IEnumerable<Entity>>> GetClassAttendance(string classId)
        {
            var _class = db.Classes.FirstOrDefault(x => x.Id == classId);

            if (_class == null)
            {
                return new BaseResponse<IEnumerable<Entity>>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = $"There is no class weith that id: {classId}"
                };
            
            }
            
            var attendances = new List<Entity>();
            foreach (var schoolKidId in _class.SchoolKidIds)
            {
                var attendance = db.Attendances.FirstOrDefault(x => x.Id == schoolKidId);
                attendances.Add(attendance);
            }

            return new BaseResponse<IEnumerable<Entity>>()
            {
                Data = attendances,
                StatusCode = StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<Entity>> Post(Entity entity)
        {
            var atten = await db.Attendances.FirstOrDefaultAsync(x => x.Id == entity.Id);
            var schoolkid = db.SchoolKids.FirstOrDefault(x => x.Id == entity.Id);
            if (atten != null || schoolkid==null)
            {
                return new BaseResponse<Entity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            if (entity.Attendance == SchoolKidAttendanceType.Unknown)
            {
                return new BaseResponse<Entity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "Schoolkid here or not?"
                };
            }

            db.Attendances.Add(entity);
            db.SaveChanges();
            return new BaseResponse<Entity>()
            {
                Data = atten,
                StatusCode = StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<Entity>> Put(string id, SchoolKidAttendanceType attendance)
        {
            var atten = await db.Attendances.FirstOrDefaultAsync(x => x.Id == id);
            if (atten == null)
            {
                return new BaseResponse<Entity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "govna poesh"
                };
            }

            if (attendance == SchoolKidAttendanceType.Unknown)
            {
                return new BaseResponse<Entity>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = "Schoolkid here or not?"
                };
            }

            atten.Attendance = attendance;
            db.Attendances.Update(atten);
            db.SaveChanges();
            return new BaseResponse<Entity>()
            {
                Data = atten,
                StatusCode = StatusCode.OK,
                Description = "Ok"
            };
        }

        public async Task<BaseResponse<Entity>> ToDefault()
        {
            foreach (var i in db.Attendances)
            {
                i.Attendance = SchoolKidAttendanceType.Unknown;
                db.Attendances.Update(i);
            }
            await db.SaveChangesAsync();

            return new BaseResponse<Entity>()
            {
                StatusCode = Domain.StatusCode.OK,
                Description = "Ok"
            };
        }

     
    }
}
