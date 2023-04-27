using System.Runtime.InteropServices.ComTypes;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAccountService _accountService;

        public ClassService(ILogger<ClassService> logger, AplicationDbContext context, IAccountService accountService)
        {
            db = context;
            _logger = logger;
            _accountService = accountService;
        }

        public Class CreateClass(Class _class)
        {
            var classEntity = _class.ToEntity();
            var teacher = _accountService.GetPerson(classEntity.TeacherId);
            if (teacher != null && teacher.GetSubClass() is not Teacher)
                throw new Exception($"{classEntity.TeacherId} not a teacher");
            db.Classes.Add(_class.ToEntity());
            db.SaveChanges();
            classEntity.SchoolKidIds?.ToList().ForEach(x =>
            {
                var schoolKid = _accountService.GetPerson(x)?.GetSubClass();
                if (schoolKid is null or not SchoolKid)
                    throw new Exception($"{x} not a schoolKid");
                if (schoolKid.ToEntity().Class == null)
                {
                    _class.AddSchoolKid(schoolKid);
                    db.ChangeTracker.Clear();
                    db.SchoolKids.Update(schoolKid.ToEntity());
                }
            });
            _class.LoadSchoolKids(db.SchoolKids.ToList());
            db.ChangeTracker.Clear();
            db.Classes.Update(_class.ToEntity());
            db.SaveChanges();
            return _class;
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
                    var _class = db.Classes
                        .Include(x => x.SchoolKids)
                        .FirstOrDefault(c => c.Id == classId);
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
            var classEntity = _class.ToEntity();
            var teacher = _accountService.GetPerson(classEntity.TeacherId).GetSubClass();
            if (teacher is not Teacher)
                throw new Exception($"{classEntity.TeacherId} not a teacher");
            classEntity.SchoolKidIds?.ForEach(x =>
            {
                if (_accountService.GetPerson(x).GetSubClass() is not SchoolKid)
                    throw new Exception($"{x} not a schoolKid");
            });
            db.Update(classEntity);
            db.SaveChanges();
            return _class;
        } 
        public IEnumerable<Class> GetClasses()
        {
            var data = db.Classes
                .Select(x => x
                    .ToInstance()
                    .LoadSchoolKids(db.SchoolKids.ToList()))
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
                return _class.ToInstance().LoadSchoolKids(db.SchoolKids.ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[GetClasses]: {exception.Message}");
                throw new RuntimeBinderException();
            }
        }
        public Class GetTeacherClass(string teacherId)
        {
            var teacher = _accountService.GetPerson(teacherId).GetSubClass();
            if (teacher is not Teacher)
                throw new Exception($"{teacherId} not a teacher");
            var _class = db.Classes.FirstOrDefault(c => c.TeacherId == teacherId);

            return _class.ToInstance().LoadSchoolKids(db.SchoolKids.ToList());
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