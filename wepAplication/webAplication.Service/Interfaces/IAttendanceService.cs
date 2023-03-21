using webAplication.Domain;

namespace webAplication.Service.Interfaces
{
    public interface IAttendanceService
    {
        Task<BaseResponse<SchoolKidAttendance.Entity>> Get(string id);
        Task<BaseResponse<SchoolKidAttendance.Entity>> ToDefault();
        Task<BaseResponse<SchoolKidAttendance.Entity>> Post(SchoolKidAttendance.Entity entity);
        Task<BaseResponse<SchoolKidAttendance.Entity>> Put(string id, SchoolKidAttendance.SchoolKidAttendanceType attendance);
        Task<BaseResponse<IEnumerable<SchoolKidAttendance.Entity>>> GetClassAttendance(string classId);
    }
}
