using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<SchoolKid>> CreateSchoolKid(SchoolKid schoolKid);

        Task<BaseResponse<IEnumerable<SchoolKid>>> GetTrustesSchoolKids(string trusteeId);

        Task<BaseResponse<Trustee>> PutSchoolKidIntoTrustee(string trusteeId, string schoolKidId);

        Task<BaseResponse<IEnumerable<SchoolKid>>> GetSchoolKids();

        Task<BaseResponse<IEnumerable<Trustee>>> GetTrustees();
    }
}
