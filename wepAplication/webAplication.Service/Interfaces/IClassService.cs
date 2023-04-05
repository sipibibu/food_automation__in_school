/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;
using webAplication.Domain.Persons;

namespace webAplication.Service.Interfaces
{
    public interface IClassService
    {
        Task<BaseResponse<Class>> CreateClass(Class _class);
        Task<BaseResponse<Class>> DeleteClasses(string[] classIds);
        Class UpdateClass(Class _class);
        Task<BaseResponse<List<Class>>> GetClasses();
        Class GetClass(string classId);
        Task<BaseResponse<Class>> GetTeacherClass(string teacherId);
        public Task<BaseResponse<Class>> AddSchoolKid(Class _class, SchoolKid schoolKid);
    }
}
*/