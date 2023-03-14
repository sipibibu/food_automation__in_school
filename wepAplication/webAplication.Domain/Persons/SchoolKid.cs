using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person
    {
        public SchoolKid(string role, string name) : base("SchoolKid", name) { }
        public void Update(SchoolKid schoolKid)
        {
            this.name = schoolKid.name;
            this.imageId = schoolKid.imageId;
        }
    }
}
