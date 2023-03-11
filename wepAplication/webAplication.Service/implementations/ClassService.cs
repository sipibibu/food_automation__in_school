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
                
                db.Classes.Add(_class);
                db.SaveChanges();

                foreach (var schoolKidId in _class.schoolKidIds)
                {
                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
                    if (schoolKid == null)
                        continue;
                    _class.schoolKids.Add((SchoolKid)schoolKid);
                }

                return new BaseResponse<Class>()
                {
                    StatusCode= StatusCode.OK,
                    Data= _class,
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
                        StatusCode= StatusCode.BAD,
                        Description= "classIds is null"
                    };
                foreach (var classId in classIds)
                {
                    var _class = db.Classes.FirstOrDefault(c => c.Id == classId);
                    if (_class == null)
                        continue;
                    db.Classes.Remove(_class);
                }
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    StatusCode= StatusCode.OK,
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
                        Description= "classId is null"
                    };
                var _classToUpdate = db.Classes.FirstOrDefault(c => c.Id == classId);

                if (_classToUpdate == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode= StatusCode.BAD,
                        Description= $"there is no class with that id:{classId}"
                    };

                _classToUpdate.Update(_class);
                db.Update(_classToUpdate);
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.OK,
                    Data= _classToUpdate
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
        public async Task<BaseResponse<IEnumerable<Class>>> GetClasses()
        {
            try
            {
                var classes = db.Classes.ToArray();

                foreach (var _class in classes)
                {
                    foreach (var schoolKidId in _class.schoolKidIds)
                    {
                        var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
                        if (schoolKid == null)
                            continue;
                        _class.schoolKids.Add((SchoolKid) schoolKid);
                    }
                }

                return new BaseResponse<IEnumerable<Class>>()
                {
                    StatusCode=StatusCode.OK,
                    Data= classes
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<IEnumerable<Class>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }
        public async Task<BaseResponse<Class>> GetClass(string classId)
        {
            try
            {
                var _class = db.Classes.FirstOrDefault(c => c.Id == classId);

                if (_class == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that id: {classId}"
                    };

                foreach (var schoolKidId in _class.schoolKidIds)
                {
                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
                    if (schoolKid == null)
                        continue;
                    _class.schoolKids.Add((SchoolKid)schoolKid);
                }

                return new BaseResponse<Class>()
                {
                    StatusCode=StatusCode.OK,
                    Data= _class,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<Class>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.BAD
                };
            }
        }

        public async Task<BaseResponse<Class>> GetTeachersClass(string teacherId)
        {
            try
            {
                var teacher = db.Person.FirstOrDefault(p => p.Id == teacherId && p.role == "teacher");
                if (teacher == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode=StatusCode.BAD,
                        Description=$"there is no teacher with that id: {teacherId}"
                    };
                var _class = db.Classes.FirstOrDefault(c => c.teacherId == teacherId);

                if (_class == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that teacherId: {teacherId}"
                    };

                foreach (var schoolKidId in _class.schoolKidIds)
                {
                    var schoolKid = db.Person.FirstOrDefault(x => x.Id == schoolKidId);
                    if (schoolKid == null)
                        continue;
                    _class.schoolKids.Add((SchoolKid)schoolKid);
                }

                return new BaseResponse<Class>()
                {
                    StatusCode=StatusCode.OK,
                    Data= _class,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<Class>()
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
