using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;

namespace webAplication.Service.Interfaces
{
    public interface IAttendanceService
    {
        Task<BaseResponse<SchoolKidAttendance>> Get(string id);
        Task<BaseResponse<SchoolKidAttendance>> ToDefault();
        Task<BaseResponse<SchoolKidAttendance>> Post(string id, SchoolKidAttendanceType attendance);
        Task<BaseResponse<SchoolKidAttendance>> Put(string id, SchoolKidAttendanceType attendance);
    }
}
