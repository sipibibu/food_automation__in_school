using webAplication.Domain;

namespace webAplication.Service.Interfaces
{
    public interface IClassService
    {
        Task<BaseResponse<Class>> AddSchoolKidToClass(string classId, string schoolkidId);
        Task<BaseResponse<Class>> CreateClass(Class _class);
        Task<BaseResponse<Class>> DeleteClasses(string[] classIds);
        Task<BaseResponse<Class>> UpdateClass(Class _class, string classId);
        Task<BaseResponse<IEnumerable<Class.Entity>>> GetClasses();
        Task<BaseResponse<Class.Entity>> GetClass(string classId);
        Task<BaseResponse<Class.Entity>> GetTeachersClass(string teacherId);
    }
}
