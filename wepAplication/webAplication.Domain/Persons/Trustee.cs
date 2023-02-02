using webAplication.Persons;

namespace webAplication.Domain.Persons
{
    public class Trustee : Person
    {
        public Trustee(string role, string name) : base(role, name) {
            schoolKidIds = new List<string>();
        }

        public List<string> schoolKidIds { get; set; }
    }
}
