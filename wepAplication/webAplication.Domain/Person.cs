using webAplication.Domain.Interfaces;

namespace webAplication.Models
{
    public class Person
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id { get {  return _id; } }
        public string name { get; private set; }

        private readonly IRole _role;
        public IRole Role { get { return _role; } }

        public Person()
        {

        }
        public Person(IRole role, string name)
        {
            this.name = name;
            this._role = role;
        }
    }
}
