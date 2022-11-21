using webAplication.Persons;

namespace webAplication.Domain.Persons
{
    public class Trustee : Person
    {
        public Trustee(string role, string name) : base(role, name) { }

        public List<SchoolKid> schoolKids = new List<SchoolKid>();
    }
}
