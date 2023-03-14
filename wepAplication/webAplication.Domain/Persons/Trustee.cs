using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Trustee : Person
    {
        public Trustee(string role, string name) : base(role, name) {
            schoolKidIds = new List<string>();
        }
        public Trustee(TrusteeEntity entity):base(entity)
        {
            this.schoolKidIds = entity.schoolKidIds;
        }

        public override TrusteeEntity ToEntity()
        {
            return new TrusteeEntity()
            {
                Id = this.Id,
                Name = this.name,
                schoolKidIds = this.schoolKidIds,
                Role = this.role,
                ImageId = this.imageId
            };
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
