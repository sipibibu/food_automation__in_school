using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using webAplication.DAL;
using webAplication.Service.Interfaces;
using webAplication.Service.Models;
using webAplication.Domain;
using webAplication.Domain.Persons;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace webAplication.Service;

/*
 Дети создаются вместе с посещяемостью
 */
public class AccountService : IAccountService
{
    private AplicationDbContext db;

    private readonly ILogger<AccountService> _logger;

    public AccountService(ILogger<AccountService> logger, AplicationDbContext context)
    {
        db = context;
        _logger = logger;
    }

    public async Task<BaseResponse<Parent.Entity>> PutSchoolKidsIntoParent(string trusteeId, string[] schoolKidIds)
    {
        try
        {
            var schoolKids = schoolKidIds
                .Select(scId => db.SchoolKids.FirstOrDefault(x => x.Id == scId)?.ToInstance())
                .ToList();

            var parent = db.Parents.FirstOrDefault(x => x.Id == trusteeId)?.ToInstance();
            parent?.ReplaceSchoolKids(schoolKids);

            db.SaveChanges();

            return new BaseResponse<Parent.Entity>()
            {
                StatusCode = StatusCode.OK,
                Data = parent.ToEntity(),
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[PutSchoolKidsIntoParent]: {exception.Message}");
            return new BaseResponse<Parent.Entity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<SchoolKid.Entity>>> GetParentSchoolKids(string parentId)
    {
        try
        {
            var parent = db.Parents.FirstOrDefault(x => x.Id == parentId)?.ToInstance();

            return new BaseResponse<IEnumerable<SchoolKid.Entity>>
            {
                Data = parent.GetSchoolKidsEntities(),
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[GetParentSchoolKids]: {exception.Message}");
            return new BaseResponse<IEnumerable<SchoolKid.Entity>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    public async Task<BaseResponse<SchoolKid.Entity>> CreateSchoolKid(SchoolKid.Entity schoolKidEntity)
    {
        //todo add validation
        try
        {
            db.SchoolKids.Add(schoolKidEntity);
            db.Attendances.Add(new SchoolKidAttendance.Entity(schoolKidEntity));
            await db.SaveChangesAsync();

            return new BaseResponse<SchoolKid.Entity>()
            {
                StatusCode = StatusCode.OK,
                Data = schoolKidEntity,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[CreateSchoolKid]: {exception.Message}");
            return new BaseResponse<SchoolKid.Entity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    public async Task<BaseResponse<User.Entity>> Register(RegisterViewModel model)
    {
        try
        {
            User user;
            switch (model.role)
            {
                case "admin":
                    user = User.GenerateRandom(
                        new Admin(model.name));
                    break;
                case "parent":
                    user = User.GenerateRandom(
                        new Parent(model.name));
                    break;
                case "canteenEmployee":
                    user = User.GenerateRandom(
                        new CanteenEmployee(model.name));
                    break;
                case "teacher":
                    user = User.GenerateRandom(
                        new Teacher(model.name));
                    break;
                case "schoolKid":
                    user = User.GenerateRandom(
                        new SchoolKid(model.name));
                    break;
                default:
                    return new BaseResponse<User.Entity>()
                    {
                        StatusCode = StatusCode.BAD,
                        Description = $"not avalible role: {model.role}"
                    };
            }

            db.Users.Add(user.ToEntity());
            db.SaveChanges();
            return new BaseResponse<User.Entity>()
            {
                Data = user.ToEntity(),
                Description = "User added",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[Register]: {exception.Message}");
            return new BaseResponse<User.Entity>()
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
            var userE = db.Users.Include(u => u.Person).FirstOrDefault(x => x.Login == model.Login);
            var user = userE?.ToInstance();
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "User not found"
                };
            }

            if (!user.IsCorrectPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Description = "Wrong password"
                };
            }

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[Login]: {ex.Message}");
            return new BaseResponse<ClaimsIdentity>
            {
                Description = ex.Message,
                StatusCode = StatusCode.BAD
            };
        }
    }

    private ClaimsIdentity Authenticate(User user)
    {
        var claims = user.GetClaim();
        return new ClaimsIdentity(claims);
    }

    public Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model)
    {
        throw new NotImplementedException();
    }

    public BaseResponse<IEnumerable<string>> GetPersons(string role)
    {
        var persons = new List<string>();
        switch (role)
        {
            case "admin":
                foreach (var person in db.Admins)
                {
                    persons.Add(JsonConvert.SerializeObject(person));
                }

                break;
            case "schoolKid":
                foreach (var person in db.SchoolKids)
                {
                    persons.Add(JsonConvert.SerializeObject(person));
                }

                break;
            case "canteenEmployee":
                foreach (var person in db.CanteenEmployees)
                {
                    persons.Add(JsonConvert.SerializeObject(person));
                }

                break;
            case "teacher":
                foreach (var person in db.Teachers)
                {
                    persons.Add(JsonConvert.SerializeObject(person));
                }

                break;
            case "parent":
                foreach (var admin in db.Parents)
                {
                    persons.Add(JsonConvert.SerializeObject(admin));
                }

                break;
        }

        return new BaseResponse<IEnumerable<string>>()
        {
            StatusCode = StatusCode.OK,
            Data = persons,
        };
    }

    public void UpdatePerson(dynamic person)
    {
        if (person is not Person) throw new NotImplementedException();
        db.Person.Update(person?.ToEntity());
        db.SaveChanges();
    }

    public void UpdateUserLogin(User user, string login)
    {
        user.SetLogin(db.Users, login);
        db.ChangeTracker.Clear();
        db.Update(user.ToEntity());
        db.SaveChanges();
    }
    
    public void UpdateUserPassword(User user, string password)
    {
        user.SetPassword(password);
        db.ChangeTracker.Clear();
        db.Update(user.ToEntity());
        db.SaveChanges();
    }

    public User GetUser(string id)
    {
        var user = db.Users.Include(x => x.Person).FirstOrDefault(x => x.Id == id);
        return user.ToInstance();
    }
    public User GetUserLocal(string id)
    {
        var user = db.Users.Local.FirstOrDefault(x => x.Id == id);
        return user.ToInstance();
    }

    public BaseResponse<string> DeletePerson(string personId)
     {
         var person = db.Person.FirstOrDefault(x => x.Id == personId);
         if (person == null)
             return new BaseResponse<string>()
             {
                 StatusCode = StatusCode.OK,
                 Description = $"there is no person with that id: {personId}"
             };
    /*    if (person.Role == "schoolKid")
            db.Attendances.Remove(db.Attendances.FirstOrDefault(x => x.Id == person.Id));*/
         db.Person.Remove(person);
         db.SaveChanges();
         return new BaseResponse<string>()
         {
             StatusCode = StatusCode.OK,
             Data = JsonConvert.SerializeObject(person),
         };
     }
    // public async Task<BaseResponse<String>> SetEmail(string userId,string email)
    // {
    //     try
    //     {
    //         var user = UserEntity.getUserAsync(db.Users, userId);
    //         if (user == null)
    //         {
    //             return new BaseResponse<String>
    //             {
    //                 Description = "User not found",
    //                 StatusCode = StatusCode.BAD,
    //             };
    //         }
    //         return new BaseResponse<String>
    //         {
    //             Description = "User not found",
    //             StatusCode = StatusCode.BAD,
    //         };
    //     }
    //     catch (Exception exception)
    //     {
    //         _logger.LogError(exception, $"[SetEmail]: {exception.Message}");
    //         return new BaseResponse<String>()
    //         {
    //             Description = exception.Message,
    //             StatusCode = StatusCode.BAD
    //         };
    //     }
    // }

    // public async Task<BaseResponse<Person>> PutImage(string personId, string imageId)
    // {
    //     try
    //     {
    //         var person = await db.Person.FirstOrDefaultAsync(p => p.Id == personId);
    //         var file = await db.Files.FirstOrDefaultAsync(f => f.Id == imageId);
    //
    //         if (person == null)
    //             return new BaseResponse<Person>()
    //             {
    //                 StatusCode = StatusCode.BAD,
    //                 Description= $"there is no person with that id: {personId}"
    //             };
    //         if (file == null)
    //             return new BaseResponse<Person>()
    //             {
    //                 StatusCode = StatusCode.BAD,
    //                 Description= $"there is no file with that id: {imageId}"
    //             };
    //
    //         person.ImageId = imageId;
    //         db.SaveChanges();
    //         return new BaseResponse<Person>()
    //         {
    //             StatusCode = StatusCode.OK,
    //         };
    //     }
    //     catch (Exception exception)
    //     {
    //         _logger.LogError(exception, $"[PutImage]: {exception.Message}");
    //         return new BaseResponse<Person>()
    //         {
    //             Description = exception.Message,
    //             StatusCode = StatusCode.BAD
    //         };
    //     }
    // }
}