namespace webAplication.Models
{
    public class User
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id { get { return _id; } }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public Person Person { get; set; } //must be private set but i retard
        public string PersonId { get; set; }

        public User(Person person)
        {
            this.Person = person;
            this.PersonId = person.Id.ToString();
            generateLogin();
            generatePassword();
        }

        public User()
        {
        }

        private void generateLogin(int loginLen=10)
        {
            Login = generateString(loginLen);
        }
        private void generatePassword(int passwordLen=10)
        {
            Password = generateString(passwordLen);
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
    }
}
