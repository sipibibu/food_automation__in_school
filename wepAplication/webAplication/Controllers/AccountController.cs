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
            return await _accountService.GetTrustesSchoolKids(trusteeId);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Trustee>> PutSchoolKidIntoTrustee(string trusteeId, string[] schoolKidIds)
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
        public async Task<BaseResponse<IEnumerable<Trustee>>> GetTrustees()
        {
            return await _accountService.GetTrustees();
        }
    }
}
