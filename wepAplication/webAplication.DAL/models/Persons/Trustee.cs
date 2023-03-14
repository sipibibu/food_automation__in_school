using webAplication.Persons;

namespace webAplication.Domain.Persons
{
    public class Trustee : Person
    {
        public Trustee(string role, string name) : base(role, name) {
            schoolKidIds = new List<string>();
        }

        public void Update(Trustee trustee)
        {
            this.name = trustee.name;
            this.schoolKidIds = trustee.schoolKidIds;
            this.imageId = trustee.imageId;
        }

        public List<string> schoolKidIds { get; set; }
    }
}
