/*using Microsoft.Extensions.Logging;
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
                
                db.Classes.Add(_class.ToEntity());
                db.SaveChanges();
                
                _class.LoadSchoolKids(db.SchoolKids);

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
                var _classToUpdate = db.Classes.FirstOrDefault(c => c.Id == classId).ToInstance();

                if (_classToUpdate == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode= StatusCode.BAD,
                        Description= $"there is no class with that id:{classId}"
                    };

                 _classToUpdate.LoadSchoolKids(db.SchoolKids);
                 _classToUpdate.Update(_class);
                db.Update(_classToUpdate);
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    StatusCode = StatusCode.OK,
                    Data = _classToUpdate
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
        public async Task<BaseResponse<List<Class>>> GetClasses()
        {
            try
            {
                var data = db.Classes
                    .Select(x => x
                        .ToInstance()
                        .LoadSchoolKids(db.SchoolKids))
                    .ToList();
                return new BaseResponse<List<Class>>()
                {
                    StatusCode=StatusCode.OK,
                    Data= data
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                return new BaseResponse<List<Class>>()
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

                return new BaseResponse<Class>()
                {
                    StatusCode=StatusCode.OK,
                    Data= _class.ToInstance().LoadSchoolKids(db.SchoolKids),
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
        public async Task<BaseResponse<Class>> GetTeacherClass(string teacherId)
        {
            try
            {
                var teacher = db.Teachers.FirstOrDefault(p => p.Id == teacherId);
                if (teacher == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode=StatusCode.BAD,
                        Description=$"there is no teacher with that id: {teacherId}"
                    };
                
                var _class = db.Classes.FirstOrDefault(c => c.TeacherId == teacherId);
                if (_class == null)
                    return new BaseResponse<Class>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"there is no class with that teacherId: {teacherId}"
                    };

                return new BaseResponse<Class>()
                {
                    StatusCode=StatusCode.OK,
                    Data= _class.ToInstance().LoadSchoolKids(db.SchoolKids),
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
}*/