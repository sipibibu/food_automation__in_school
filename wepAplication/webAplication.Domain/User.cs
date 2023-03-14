using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.CSharp.RuntimeBinder;
using webAplication.DAL.models;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class User
    {
        private string _id;
        private string _login;
        private string _password;
        private string _personId { get; set; }
        public Person? Person { get; set; }

        public static User GenerateRandom(Person person)
        {
            var user = new User();
            user.Person = person;
            user._personId = person.Id;
            user.GenerateLogin();
            user.GeneratePassword();
            return user;
        }

        private User() { }
        public User(Person person, string password)
        {
            this.Person = person;
            this._personId = person.Id.ToString();
            this._password = password;
            _login = "string";
        }

        private void GenerateLogin(int loginLen = 10)
        {
            _login = GenerateString(loginLen);
        }
        private void GeneratePassword(int passwordLen = 10)
        {
            _password = GenerateString(passwordLen);
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
        public static User? GetUser(IList<UserEntity> users, string login)
        {
            UserEntity? userEntity = users.FirstOrDefault(x => x.Login == login);
            return userEntity != null ? FromEntity(userEntity) : null; 
        }
        private static User FromEntity(UserEntity userEntity)
        {
            if (userEntity is null)
                throw new RuntimeBinderException("userEntity was null");
            return new User(userEntity);
        }
        private UserEntity ToEntity()
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
            new Claim("role", user.Person.role)
            };
        }
    }
}
