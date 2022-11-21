using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webAplication.Domain;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;
using AplicationDbContext = webAplication.DAL.AplicationDbContext;

namespace webAplication.Service;

public class AccountService : IAccountService
{
    private AplicationDbContext db;

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

    [Authorize(Roles = "admin")]
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

            switch (model.role)
            {
                case "admin":
                    user = new User(
                            new Admin(model.role, model.Login));
                    break;
                case "trustee":
                    user = new User(
                        new Trustee(model.role, model.Login));
                    break;
                case "canteenEmploee":
                    user = new User(
                        new CanteenEmploee(model.role, model.Login));
                    break;
                case "teacher":
                    user = new User(
                        new Teacher(model.role, model.Login));
                    break;
                    throw new ArgumentException($"Role {model.role} dosent exists");
            }

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

    [Authorize(Roles = "admin")]
    public async Task<BaseResponse<Trustee>> PutSchoolKidIntoTrustee(string trusteeId, string schoolKidId)
    {
        try
        {
            Trustee trustee = await db.Trustees.FirstOrDefaultAsync(x => x.Id == trusteeId);
            SchoolKid schoolKid = await db.SchoolKids.FirstOrDefaultAsync(x => x.Id == schoolKidId);


            trustee.schoolKids.Add(schoolKid);

            db.SaveChanges();

            return new BaseResponse<Trustee>()
            {
                StatusCode = StatusCode.OK,
                Data = trustee,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[Register]: {exception.Message}");
            return new BaseResponse<Trustee>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    [Authorize(Roles = "admin")]
    public async Task<BaseResponse<IEnumerable<SchoolKid>>> GetTrustesSchoolKids(string trusteeId)
    {
        try
        {
            Trustee trustee = await db.Trustees.Include(x => x.schoolKids).FirstOrDefaultAsync(x => x.Id == trusteeId);

            if (trustee == null)
            {
                return new BaseResponse<IEnumerable<SchoolKid>>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = "There is no Trustee with that id"
                };
            }

            return new BaseResponse<IEnumerable<SchoolKid>>
            {
                Data = trustee.schoolKids.ToList()
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[Register]: {exception.Message}");
            return new BaseResponse<IEnumerable<SchoolKid>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    [Authorize(Roles = "admin")]
    public async Task<BaseResponse<SchoolKid>> CreateSchoolKid(SchoolKid schoolKid)
    {
        //todo add validation
        try
        {
            db.SchoolKids.AddAsync(schoolKid);
            db.SaveChangesAsync();

            return new BaseResponse<SchoolKid>()
            {
                StatusCode = StatusCode.OK,
                Data = schoolKid,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[Register]: {exception.Message}");
            return new BaseResponse<SchoolKid>()
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
    private ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            //new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Person.role)
            new Claim("name", user.Login),
            new Claim("role", user.Person.role)

        };
        return new ClaimsIdentity(claims); //
    }
    public Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<Trustee>> RemoveSchoolKidFromTrustee(string trusteeId, string[] SchoolKidIds)
    {
        try
        {
            var trustee = await db.Trustees.Include(t => t.schoolKids).FirstOrDefaultAsync(x => x.Id == trusteeId);

            if (trustee == null)
            {
                return new BaseResponse<Trustee>()
                {
                    StatusCode = StatusCode.BAD,
                    Description = "trusteeId is null"
                };
            }

            foreach (var item in trustee.schoolKids)
            {
                if (item.Id == trusteeId)
                    trustee.schoolKids.Remove(item);
            }

            return new BaseResponse<Trustee>()
            {
                StatusCode= StatusCode.OK,
                Data = trustee
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[GetSchoolKids]: {exception.Message}");
            return new BaseResponse<Trustee> ()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }
    public async Task<BaseResponse<IEnumerable<SchoolKid>>> GetSchoolKids()
    {
        try
        {
            var schoolKids = db.SchoolKids.ToList();

            return new BaseResponse<IEnumerable<SchoolKid>>()
            {
                StatusCode = StatusCode.OK,
                Data = schoolKids,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[GetSchoolKids]: {exception.Message}");
            return new BaseResponse<IEnumerable<SchoolKid>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<Trustee>>> GetTrustees()
    {
        try
        {
            var trustees = db.Trustees.ToList();

            return new BaseResponse<IEnumerable<Trustee>>()
            {
                StatusCode = StatusCode.OK,
                Data = trustees,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[GetTrustees]: {exception.Message}");
            return new BaseResponse<IEnumerable<Trustee>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }
}