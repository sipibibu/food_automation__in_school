using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webAplication.Domain;
using webAplication.Domain.Persons;
using webAplication.Service.Models;

namespace webAplication.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<JwtSecurityTokenHandler>> RefreshToken(RegisterViewModel model);
        BaseResponse<User.Entity> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        //Task<BaseResponse<String>> SetEmail(string userId, string email);
        IEnumerable<SchoolKid> GetParentSchoolKids(string parentId);
        Parent PutSchoolKidsIntoParent(Parent parent, SchoolKid?[] schoolKids);
        Person? GetPerson(string id);
        IEnumerable<Person> GetPersons(string role);
        BaseResponse<string> DeletePerson(string personId);
        void UpdatePerson(dynamic person);
        void UpdateUserLogin(User user, string login);
        public void UpdateUserPassword(User user,string password);

        public User GetUser(string id);
        public List<User> GetUsers();
        public User GetUserLocal(string id); 
        //Task<BaseResponse<Person>> PutImage(string personId, string imageId);
    }
}
