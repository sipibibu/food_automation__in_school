using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using JsonKnownTypes;
using Newtonsoft.Json;
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
        [JsonProperty("Id")]
        private string _id;
        [JsonProperty("Login")]
        private string _login;
        [JsonProperty("Password")]
        private string _password;
        [JsonProperty("Person")]
        private Person _person;

        private User()
        {
            _id = Guid.NewGuid().ToString();
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
        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = _id,
                Login = _login,
                Password = _password,
                Person = _person?.GetPerson().ToEntity(),
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
        public static bool IsLoginUniq(DbSet<User.Entity> users, string login)
        {
            return !(users.FirstOrDefault(x => x.Login == login) is User);
        }
        public void UpdatePerson(DbSet<Person.Entity> persons)
        {
            _person = persons.FirstOrDefault(x => x.UserId == _id).GetPerson().ToInstance();
        }
        public void SetLogin(DbSet<User.Entity> users,string login)
        {
            if (IsLoginUniq(users, login))
            {
                //_person = null;
                _login = login;
                return;
            }
            throw new Exception();
        }
        public void SetPassword(string password)
        {
            if (!password.Equals(null))
                _password = password;
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
