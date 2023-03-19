using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class User : IInstance<User.Entity>
    {
        public class Entity : IInstance<User.Entity>.IEntity<User>
        {
            [Key]
            public string Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public Person.Entity Person { get; set; }
            public Entity() { }
            public User ToInstance()
            {
                return new User(this);
            }
        }
        private string _id;
        private string _login;
        private string _password;
        private Person _person;

        private User()
        {
            throw new Exception();
        }
        public static User GenerateRandom(Person person)
        {
            var user = new User();
            user._person = person;
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

        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = _id,
                Login = _login,
                Password = _password,
                Person = _person.ToEntity(),
            };
        }

        private User(Entity userEntity)
        {
            _id = userEntity.Id;
            _login = userEntity.Login;
            _password = userEntity.Password;
            _person = userEntity.Person.GetPerson().ToInstance();
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

        public List<Claim> GetClaim()
        {
            return new List<Claim>{
                new Claim("login", _login),
                _person.GetClaim(),
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
