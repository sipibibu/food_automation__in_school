using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.CSharp.RuntimeBinder;
using webAplication.DAL.Interfaces;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class User : IInstance, ITransferred<UserEntity, User>
    {
        private string _id;
        private string _login;
        private string _password;
        private string _personId;
        public Person? Person { get; set; }

        private User()
        {
            throw new Exception();
        }
        public static User GenerateRandom(Person person)
        {
            var user = new User();
            user.Person = person;
            user._personId = person.Id;
            user.GenerateLogin();
            user.GeneratePassword();
            return user;
        }
        private string GenerateString(int strLen)
        {
            string str = "";
            Random random = new Random();
            for (int i = 0; i < strLen; i++)
            {
                int randInt = random.Next(0, 62);
                if (randInt < 10)
                    randInt += 48;
                else if (randInt < 36)
                    randInt += 65 - 10;
                else
                    randInt += 97 - 36;
                str += ((char)randInt).ToString();
            }
            return str;
        }

        public static Task<User?> GetUserAsync(DbSet<User> users, string login)
        {
            throw new NotImplementedException("");
        }
        public static User ToInstance(UserEntity userEntity)
        {
            if (userEntity is null)
                throw new RuntimeBinderException("userEntity was null");
            return new User(userEntity);
        }

        public UserEntity ToEntity()
        {
            return new UserEntity()
            {
                Id = _id,
                Login = _login,
                Password = _password,
                PersonId = _personId,
            };
        }

        private User(UserEntity userEntity)
        {
            _id = userEntity.Id;
            _login = userEntity.Login;
            _password = userEntity.Password;
            _personId = userEntity.PersonId;
        }
        public bool IsCorrectPassword(string password)
        {
            return _password.Equals(password);
        }

        public static bool IsLoginUniq(DbSet<User> users, string login)
        {
            return users.FirstOrDefaultAsync(x => x._login == login) == null;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            var user = obj as User;
            return _login.Equals(user._login) && _password.Equals(user._password);
        }

        public List<Claim> GetClaim(User user)
        {
            return new List<Claim>{
            new Claim("name", user._login),
            };
        }

        private void GenerateLogin(int loginLen = 10)
        {
            _login = GenerateString(loginLen);
        }
        private void GeneratePassword(int passwordLen = 10)
        {
            _password = GenerateString(passwordLen);
        }
    }
}
