using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webAplication.DAL.models;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class User
    {
        private string _id = Guid.NewGuid().ToString();
        public string Id { get { return _id; } }
        private string login;
        private string password;
        public Person Person { get; set; } //must be private set but i retard
        public string PersonId { get; set; }
        public static User GenerateRandom(Person person)
        {
            var user = new User();
            user.Person = person;
            user.PersonId = person.Id.ToString();
            user.generateLogin();
            user.generatePassword();
            return user;
        }
        public User(Person person, string password)
        {
            this.Person = person;
            this.PersonId = person.Id.ToString();
            this.password = password;
            login = "string";
        }
        public User() { }
        private void generateLogin(int loginLen = 10)
        {
            login = generateString(loginLen);
        }
        private void generatePassword(int passwordLen = 10)
        {
            password = generateString(passwordLen);
        }
        private string generateString(int strLen)
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
        public static Task<User?> getUserAsync(DbSet<User> users, string login)
        {
            return users.Include(x => x.Person).FirstOrDefaultAsync(x => x.login == login);
        }
        public static User? getUser(DbSet<User> users, string login)
        {
            return users.Include(x => x.Person).FirstOrDefault(x => x.login == login);
        }
        public bool isCorrectPassword(string password)
        {
            return this.password.Equals(password.GetHashCode());
        }
        public static bool IsLoginUniq(DbSet<User> users, string login)
        {
            return users.FirstOrDefaultAsync(x => x.login == login) == null;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            var user = obj as User;
            return login.Equals(user.login) && password.Equals(user.password);
        }
        public List<Claim> GetClaim(User user)
        {
            return new List<Claim>{
            new Claim("name", user.login),
            new Claim("role", user.Person.role)
            };
        }
    }
}
