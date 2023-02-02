using webAplication.Persons;

namespace webAplication.Domain.Persons
{
    public class Admin : Person
    {
        public int balance;
        public Admin(string role, string name) : base(role, name) { }
    }
}
