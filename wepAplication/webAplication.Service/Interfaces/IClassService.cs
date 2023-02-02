using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;

namespace webAplication.Service.Interfaces
{
    public interface IClassService
    {
        Task<BaseResponse<Class>> CreateClass(Class _class);
        Task<BaseResponse<Class>> DeleteClasses(string[] classIds);
        Task<BaseResponse<Class>> UpdateClass(Class _class, string classId);
        Task<BaseResponse<IEnumerable<Class>>> GetClasses();
        Task<BaseResponse<Class>> GetClass(string classId);
        Task<BaseResponse<Class>> GetTeachersClass(string teacherId);
    }
}
