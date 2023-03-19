using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webAplication.DAL.models;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model);
        Task<BaseResponse<UserEntity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<BaseResponse<String>> SetEmail(string userId, string email);
        Task<BaseResponse<SchoolKid>> CreateSchoolKid(SchoolKid schoolKid);
        Task<BaseResponse<IEnumerable<SchoolKid>>> GetParentSchoolKids(string parentId);
        Task<BaseResponse<Parent>> PutSchoolKidsIntoParent(string trusteeId, string[] schoolKidIds);
        Task<BaseResponse<IEnumerable<SchoolKid>>> GetSchoolKids();
        Task<BaseResponse<IEnumerable<Teacher>>> GetTeachers();
        Task<BaseResponse<IEnumerable<CanteenEmployee>>> GetCanteenEmployees();
        Task<BaseResponse<Parent>> UpdateTrustee(Parent trustee, string id);
        Task<BaseResponse<CanteenEmployee>> UpdateCanteenEmployee(CanteenEmployee canteenEmployee, string id);
        Task<BaseResponse<Teacher>> UpdateTeacher(Teacher teacher, string id);
        Task<BaseResponse<SchoolKid>> UpdateSchoolKid(SchoolKid schoolKid, string id);
        Task<BaseResponse<SchoolKid>> DeleteSchoolKid(string id);
        Task<BaseResponse<CanteenEmployee>> DeleteCanteenEmployee(string id);
        Task<BaseResponse<Parent>> DeleteTrustee(string id);
        Task<BaseResponse<Teacher>> DeleteTeacher(string id);
        Task<BaseResponse<IEnumerable<Parent>>> GetTrustees();
        Task<BaseResponse<Person>> PutImage(string personId, string imageId);
    }
}
