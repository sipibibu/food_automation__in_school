using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Security;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Models;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;
using AplicationDbContext = webAplication.DAL.AplicationDbContext;

namespace webAplication.Service;

public class AccountService : IAccountService
{
    AplicationDbContext db;

    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger, AplicationDbContext context)
    {
        db = context;
        _logger = logger;
    }

    public async Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
    {
        try
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "user with this login already exists"
                };
            }

            user = new User(
                new Person(model.role, model.Login));

            db.Users.AddAsync(user);
            db.SaveChangesAsync();
            var result = Authenticate(user);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "User added",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[Register]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }

    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            User? user = await db.Users.Include(x => x.Person).FirstOrDefaultAsync(x => x.Login == model.Login);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "User not found"
                };
            }

            if (user.Password != model.Password)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Wrong password"
                };
            }

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>
            {
                Data= result,
                StatusCode= StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>
            {
                Description= ex.Message,
                StatusCode= StatusCode.BAD
            };
        }
    }

    /// <summary>
    /// method for Authenticate user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>new ClaimsIdentity(claims, "AplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)</returns>
    public ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Person.role)
        };
        return new ClaimsIdentity(claims, "AplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType); //
    }

    public Task<BaseResponse<ClaimsIdentity>> Logout()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model)
    {
        throw new NotImplementedException();
    }
}