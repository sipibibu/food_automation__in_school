using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Models;
using webAplication.Persons;
using webAplication.Service;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;



namespace webAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKid>> CreateSchoolKid(string name)
        {
            return await _accountService.CreateSchoolKid(new SchoolKid("",name));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<SchoolKid>>> GetTrustesSchoolKids(string trusteeId)
        {
            return await _accountService.GetParentSchoolKids(trusteeId);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Parent>> PutSchoolKidIntoTrustee(string trusteeId, string[] schoolKidIds)
        {
            return await _accountService.PutSchoolKidIntoTrustee(trusteeId, schoolKidIds);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<User>> Register(RegisterViewModel model)
        {
            var response = await _accountService.Register(model);
            if (null == response)
                return new BaseResponse<User>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                };
            return response;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _accountService.GetTeachers();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<CanteenEmployee>>> GetCanteenEmployees()
        {
            return await _accountService.GetCanteenEmployees();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<Parent>> UpdateTrustee(Parent trustee, string id)
        {
            return await _accountService.UpdateTrustee(trustee, id);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<Teacher>> UpdateTeacher(Teacher teacher, string id)
        {
            return await _accountService.UpdateTeacher(teacher, id);
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKid>> UpdateSchoolKid(SchoolKid schoolKid, string id)
        {
            return await _accountService.UpdateSchoolKid(schoolKid, id);

        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<CanteenEmployee>> UpdateCanteenEmployee(CanteenEmployee canteenEmployee, string id)
        {
            return await _accountService.UpdateCanteenEmployee(canteenEmployee, id);

        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<SchoolKid>> DeleteSchoolKid(string id)
        {
            return await _accountService.DeleteSchoolKid(id);
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<Parent>> DeleteTrustee(string id)
        {
            return await _accountService.DeleteTrustee(id);
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<Teacher>> DeleteTeacher(string id)
        {
            return await _accountService.DeleteTeacher(id);
        }
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<CanteenEmployee>> DeleteCanteenEmployee(string id)
        {
            return await _accountService.DeleteCanteenEmployee(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.StatusCode.OK)
                {
                    /*await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));*/ // cookie auth
                     
                    var now = DateTime.UtcNow;

                    var jwt = new JwtSecurityToken(
                           issuer: AuthOptions.ISSUER,
                           audience: AuthOptions.AUDIENCE,
                           notBefore: now,
                           claims: response.Data.Claims,
                           expires: now.Add(TimeSpan.FromHours(1)),
                           signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                    var obj = new
                    {
                        access_token = encodedJwt,
                    };

                    return Ok(obj);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<SchoolKid>>> GetSchoolKids()
        {
            return await _accountService.GetSchoolKids();
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<Parent>>> GetTrustees()
        {
            return await _accountService.GetTrustees();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Person>> PutImage(string personId, string imageId)
        {
            return await _accountService.PutImage(personId, imageId);
        }
    }
}
