using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        public async Task<BaseResponse<SchoolKid.Entity>> CreateSchoolKid(string name)
        {
            return await _accountService.CreateSchoolKid(new SchoolKid(name).ToEntity());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<SchoolKid.Entity>>> GetTrustesSchoolKids(string trusteeId)
        {
            return await _accountService.GetParentSchoolKids(trusteeId);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<Parent.Entity>> PutSchoolKidsIntoParent(string trusteeId, string[] schoolKidIds)
        {
            return await _accountService.PutSchoolKidsIntoParent(trusteeId, schoolKidIds);
        }
/*
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("[action]")]*/
        // public async Task<BaseResponse<User>> Register(RegisterViewModel model)
        // {
        //     var response = await _accountService.Register(model);
        //     if (null == response)
        //         return new BaseResponse<User>()
        //         {
        //             StatusCode = Domain.StatusCode.BAD,
        //         };
        //     return response;
        // }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<string>>> GetPersons(string role)
        {
            return _accountService.GetPersons(role);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string>> UpdatePerson(dynamic person)
        {
            return _accountService.UpdatePerson(person);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string>> DeletePerson(string personId)
        {
            return _accountService.DeletePerson(personId);
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

        // [HttpPut]
        // [Route("[action]")]
        // public async Task<BaseResponse<Person>> PutImage(string personId, string imageId)
        // {
        //     return await _accountService.PutImage(personId, imageId);
        // }
    }
}
