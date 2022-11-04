using webAplication.Domain.Interfaces;

namespace webAplication.Models
{
    public class Person
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id { get {  return _id; } }
        public string name { get; private set; }
        public string role { get; private set; }

        public Person()
        {
        }
        public Person(string role, string name)
        {
            this.name = name;
            this.role = role;
        }
    }
}
