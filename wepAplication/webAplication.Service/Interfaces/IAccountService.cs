using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Persons;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model);
        Task<BaseResponse<User>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<SchoolKid>> CreateSchoolKid(SchoolKid schoolKid);

        Task<BaseResponse<IEnumerable<SchoolKid>>> GetTrustesSchoolKids(string trusteeId);

        Task<BaseResponse<Trustee>> PutSchoolKidIntoTrustee(string trusteeId, string[] schoolKidIds);

        Task<BaseResponse<IEnumerable<SchoolKid>>> GetSchoolKids();

        Task<BaseResponse<IEnumerable<Teacher>>> GetTeachers();

        Task<BaseResponse<IEnumerable<CanteenEmployee>>> GetCanteenEmployees();
        Task<BaseResponse<Trustee>> UpdateTrustee(Trustee trustee, string id);

        Task<BaseResponse<CanteenEmployee>> UpdateCanteenEmployee(CanteenEmployee canteenEmployee, string id);
        Task<BaseResponse<Teacher>> UpdateTeacher(Teacher teacher, string id);
        Task<BaseResponse<SchoolKid>> UpdateSchoolKid(SchoolKid schoolKid, string id);
        Task<BaseResponse<SchoolKid>> DeleteSchoolKid(string id);
        Task<BaseResponse<CanteenEmployee>> DeleteCanteenEmployee(string id);

        Task<BaseResponse<Trustee>> DeleteTrustee(string id);
        Task<BaseResponse<Teacher>> DeleteTeacher(string id);

        Task<BaseResponse<IEnumerable<Trustee>>> GetTrustees();

        Task<BaseResponse<Person>> PutImage(string personId, string imageId);

    }
}
