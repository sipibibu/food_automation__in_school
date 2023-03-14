using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Parent : Person
    {
        public Parent(string role, string name) : base(role, name) {
            schoolKidIds = new List<string>();
        }
        public Parent(ParentEntity entity):base(entity)
        {
            this.schoolKidIds = entity.schoolKidIds;
        }

        public override ParentEntity ToEntity()
        {
            return new ParentEntity()
            {
                Id = this.Id,
                Name = this.name,
                schoolKidIds = this.schoolKidIds,
                Role = this.role,
                ImageId = this.imageId
            };
        }
        public static Parent FromEntity(ParentEntity entity)
        {
            return new Parent(entity);
        }
        public void Update(Parent trustee)
        {
            this.name = trustee.name;
            this.schoolKidIds = trustee.schoolKidIds;
            this.imageId = trustee.imageId;
        }


        public List<string> schoolKidIds { get; set; }
    }
}
