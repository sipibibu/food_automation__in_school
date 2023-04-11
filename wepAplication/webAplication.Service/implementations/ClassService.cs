using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
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
                var _classE = _class.ToEntity();
                db.Classes.Add(_classE);
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
        public Class UpdateClass(Class _class)
        {
            db.Update(_class);
            db.SaveChanges();
            return _class;
        } 
        public IEnumerable<Class> GetClasses()
        {
            var data = db.Classes
                .Select(x => x
                    .ToInstance()
                    .LoadSchoolKids(db.SchoolKids))
                .ToList();
            return data;
        }
        public Class GetClass(string classId)
        {
            try
            {
                var _class = db.Classes.FirstOrDefault(c => c.Id == classId);

                if (_class == null)
                    throw new RuntimeBinderException();
                return _class.ToInstance().LoadSchoolKids(db.SchoolKids);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                throw new RuntimeBinderException();
            }
        }
        public Class GetTeacherClass(string teacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(p => p.Id == teacherId);
            var _class = db.Classes.FirstOrDefault(c => c.TeacherId == teacherId);

            return _class.ToInstance().LoadSchoolKids(db.SchoolKids);
        }

        public async Task<BaseResponse<Class>> AddSchoolKid(Class _class, SchoolKid schoolKid)
        {
            try
            {
                _class.AddSchoolKid(schoolKid);
                db.Update(_class);
                db.SaveChanges();
                return new BaseResponse<Class>()
                {
                    Data = _class,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<Class>()
                {
                    Description = e.Message,
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