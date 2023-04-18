using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<BaseResponse<string>> GetParentSchoolKids(string trusteeId)
        {
            try
            {
                var result = _accountService.GetParentSchoolKids(trusteeId);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<BaseResponse<string>> PutSchoolKidsIntoParent(string trusteeId, string[] schoolKidIds)
        {
            try
            {
                var parent = _accountService.GetPerson(trusteeId).GetSubClass();
                var schoolKids = schoolKidIds
                    .Select(x =>
                    {
                        if (_accountService
                                .GetPerson(x)
                                .GetSubClass() is SchoolKid)
                            return _accountService
                                .GetPerson(x)
                                .GetSubClass() as SchoolKid;
                        return null;})
                    .ToArray(); 
                _accountService.PutSchoolKidsIntoParent(parent, schoolKids);
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.OK,
                    Data = JsonConvert.SerializeObject(parent)
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                    Description = e.Message
                };
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string>> Register(RegisterViewModel model)
        {
            var response = await _accountService.Register(model);
            if (null == response)
                return new BaseResponse<string>()
                {
                    StatusCode = Domain.StatusCode.BAD,
                };
            return new BaseResponse<string>()
            {
                StatusCode = Domain.StatusCode.OK,
                Data = JsonConvert.SerializeObject(response),
            };
        }


        [HttpGet]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<IEnumerable<string>>> GetPersons(string role)
        {
            var persons = _accountService.GetPersons(role);
            return new BaseResponse<IEnumerable<string>>()
            {
                Data = persons.Select(x => JsonConvert.SerializeObject(x)),
                StatusCode = Domain.StatusCode.OK
            };
        }

        [HttpPut]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string>> UpdatePerson(string person)
        {
            try
            {
                dynamic personEntity = JsonConvert.DeserializeObject<Person>(person);
                _accountService.UpdatePerson(personEntity);
                return new BaseResponse<string>()
                {
                    Data = JsonConvert.SerializeObject(personEntity),
                    StatusCode = Domain.StatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string>()
                {
                    Description = e.Message,
                    StatusCode = Domain.StatusCode.BAD,
                };
            }
        }
        
        
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string?>> GetUser(string userId)
        {
            try
            { 
                var user = _accountService.GetUser(userId);
                return new BaseResponse<string?>()
                {
                    Data = JsonConvert.SerializeObject(user),
                    StatusCode = Domain.StatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string?>()
                {
                    Description = e.Message,
                    StatusCode = Domain.StatusCode.BAD,
                };
            }
        }
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string?>> GetUsers()
        {
            try
            {
                var users = _accountService.GetUsers().Select(x => x);
                return new BaseResponse<string?>()
                {
                    Data = JsonConvert.SerializeObject(users),
                    StatusCode = Domain.StatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string?>()
                {
                    Description = e.Message,
                    StatusCode = Domain.StatusCode.BAD,
                };
            }
        }

        [HttpPut]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string?>> UpdateUserLogin(string userId, string login)
        {
            try
            { 
                var user = _accountService.GetUser(userId);
                _accountService.UpdateUserLogin(user, login);
                return new BaseResponse<string?>()
                {
                    Data = user.ToString(),
                    StatusCode = Domain.StatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string?>()
                {
                    Description = e.Message,
                    StatusCode = Domain.StatusCode.BAD,
                };
            }
        }
        [HttpPut]
        //[Authorize(Roles = "admin")]
        [Route("[action]")]
        public async Task<BaseResponse<string?>> UpdateUserPassword(string userId, string password)
        {
            try
            { 
                var user = _accountService.GetUser(userId);
                _accountService.UpdateUserPassword(user, password);
                return new BaseResponse<string?>()
                {
                    Data = user.ToString(),
                    StatusCode = Domain.StatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new BaseResponse<string?>()
                {
                    Description = e.Message,
                    StatusCode = Domain.StatusCode.BAD,
                };
            }
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
