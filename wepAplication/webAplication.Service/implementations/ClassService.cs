using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.DAL;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.Interfaces;

namespace webAplication.Service.implementations
{
    public class ClassService : IClassService
    {
        private AplicationDbContext db;

        private readonly ILogger<ClassService> _logger;

        public ClassService(ILogger<ClassService> logger, AplicationDbContext context)
        {
            db = context;
            _logger = logger;
        }
        public async Task<BaseResponse<Class>> AddSchoolKidToClass(string classId, string schoolkidId)
        {
            var _class = db.Classes.Include(x=> x.SchoolKids).FirstOrDefault(x => x.Id == classId);
            if (_class == null)
            {
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = "Class was null"
                };
            }
            var schoolkid = db.SchoolKids.FirstOrDefault(x=>x.Id==schoolkidId);
            if (schoolkid == null)
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = "Net takogo schoolkid"
                };
            _class.SchoolKids.Add(schoolkid);
            db.Update(_class);
            db.SaveChanges();
            return new BaseResponse<Class>()
            {
                StatusCode = StatusCode.OK,
                Data=_class.ToInstance(),
                Description = "Kk"
                
            };
        }


        public async Task<BaseResponse<Class>> CreateClass(Class _class)
        {
            try
            {
                if (_class == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "Class was null"
                    };

                var entity = _class.ToEntity();
                foreach (var schoolKidId in entity.SchoolKidIds)
                {
                    var schoolKid = db.SchoolKids.FirstOrDefault(x => x.Id == schoolKidId);
                    if (schoolKid == null)
                        continue;
                    entity.SchoolKids.Add(schoolKid);
                }

                db.Classes.Add(entity);
                db.SaveChanges();

                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.OK,
                    Data = _class,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CreateClass]: {exception.Message}");
                return new BaseResponse<Class>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Class>> DeleteClasses(string[] classIds)
        {
            try
            {
                if (classIds == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "classIds is null"
                    };
                foreach (var classId in classIds)
                {
                    var _class = db.Classes.FirstOrDefault(c => c.Id == classId);
                    if (_class == null)
                        continue;
                    db.Remove(_class);
                }
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CreateClass]: {exception.Message}");
                return new BaseResponse<Class>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<Class>> UpdateClass(Class _class, string classId)
        {
            try
            {
                if (classId == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = "classId is null"
                    };
                var _classToUpdate = db.Classes.FirstOrDefault(c => c.Id == classId);

                if (_classToUpdate == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that id:{classId}"
                    };

                var inst=_classToUpdate.ToInstance();
                inst.Update(_class);
                db.Update(inst.ToEntity());
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.OK,
                    Data = inst
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[CreateClass]: {exception.Message}");
                return new BaseResponse<Class>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<IEnumerable<Class.Entity>>> GetClasses()
        {
            try
            {
                var classes = db.Classes.ToArray();

                foreach (var _class in classes)
                {
                    foreach (var schoolKidId in _class.SchoolKidIds)
                    {
                        var schoolKid = db.SchoolKids.FirstOrDefault(x => x.Id == schoolKidId);
                        if (schoolKid == null)
                            continue;
                        _class.SchoolKids.Add(schoolKid);
                    }
                }

                return new BaseResponse<IEnumerable<Class.Entity>>()
                {
                    StatusCode = StatusCode.OK,
                    Data = classes
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<IEnumerable<Class.Entity>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<Class.Entity>> GetClass(string classId)
        {
            try
            {
                var _class = db.Classes.FirstOrDefault(c => c.Id == classId);

                if (_class == null)
                    return new BaseResponse<Class.Entity>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that id: {classId}"
                    };

        /*        foreach (var schoolKid in _class.SchoolKidsIds)
                {
*//*                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
*//*                    if (schoolKid == null)
                        continue;
                    _class.SchoolKids.Add(schoolKid);
                }*/

                return new BaseResponse<Class.Entity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = _class,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<Class.Entity>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Class.Entity>> GetTeachersClass(string teacherId)
        {
            try
            {
                var teacher = db.Person.FirstOrDefault(p => p.Id == teacherId && p.Role == "teacher");
                if (teacher == null)
                    return new BaseResponse<Class.Entity>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no teacher with that id: {teacherId}"
                    };
                var _class = db.Classes.FirstOrDefault(c => c.TeacherId == teacherId);

                if (_class == null)
                    return new BaseResponse<Class.Entity>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that teacherId: {teacherId}"
                    };

              /*  foreach (var schoolKidId in _class.schoolKidIds)
                {
                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
                    if (schoolKid == null)
                        continue;
                    _class.schoolKids.Add((SchoolKid)schoolKid);
                }*/

                return new BaseResponse<Class.Entity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = _class,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<Class.Entity>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        //public BaseResponse<Class> NoteAttendance(Dictionary<string, bool> attendance,  string classId)
        //{
        //    try
        //    {
        //        if (classId == null)
        //            return new BaseResponse<Class>()
        //            {
        //                StatusCode= StatusCode.BAD,
        //                Description= "classId is null"
        //            };

        //        var _class = db.Classes.FirstOrDefault(c => c.Id == classId);
        //        if (_class == null)
        //            return new BaseResponse<Class>()
        //            {
        //                StatusCode= StatusCode.OK,
        //                Description= $"there is no class with that id: {classId}"
        //            };
        //        if (attendance.Count != _class.schoolKidIds.Length)
        //            return new BaseResponse<Class>()
        //            {
        //                StatusCode= StatusCode.BAD,
        //                Description= "schoolKidIds count in class and attendance was not equals",
        //            };

        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError(exception, $"[noteAttendance]: {exception.Message}");
        //        return new BaseResponse<Class>()
        //        {
        //            Description = exception.Message,
        //            StatusCode = StatusCode.BAD
        //        };
        //    }
        //}

        public void FillAttendance()
        {

        }
    }
}
