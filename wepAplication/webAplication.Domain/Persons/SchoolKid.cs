using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person
    {
        public SchoolKid(string role, string name) : base("SchoolKid", name) { }
        public SchoolKid(SchoolKidEntity entity) : base(entity) { }
        public override SchoolKidEntity ToEntity()
        {
            return new SchoolKidEntity()
            {
                Id = this.Id,
                Name = this.name,
                Role = this.role,
                ImageId = this.imageId
            };
        }
        public static SchoolKid FromEntity(SchoolKidEntity entity)
        {
            return new SchoolKid(entity);
        }
        public void Update(SchoolKid schoolKid)
        {
            this.name = schoolKid.name;
            this.imageId = schoolKid.imageId;
        }
    }
}
