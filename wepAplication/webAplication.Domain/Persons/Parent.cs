using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Parent : Person, ITransferred<ParentEntity, Parent>
    {
        public Parent(string role, string name) : base(role, name) {
            schoolKidIds = new List<string>();
        }
        public Parent(ParentEntity entity):base(entity)
        {
            this.schoolKidIds = entity.schoolKidIds;
        }
        public ParentEntity ToEntity()
        {
            return new ParentEntity()
            {
                Id = Id,
                Name = _name,
                schoolKidIds = schoolKidIds,
                Role = _role,
                ImageId = _imageId
            };
        }
        public static Parent FromEntity(ParentEntity entity)
        {
            return new Parent(entity);
        }
        public void Update(Parent trustee)
        {
            this._name = trustee._name;
            this.schoolKidIds = trustee.schoolKidIds;
            this._imageId = trustee._imageId;
        }
        public List<string> schoolKidIds { get; set; }
    }
}
