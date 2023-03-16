using webAplication.DAL.models;
using webAplication.DAL.models.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Parent : Person, ITransferredInstance<ParentEntity, Parent>
    {
        private List<string> _schoolKidIds = new List<string>();

        private Parent(string role, string name) : base(role, name) { }

        private Parent(ParentEntity entity) : base(entity)
        {
            _schoolKidIds = entity.SchoolKidIds;
        }

        public ParentEntity ToEntity()
        {
            var person = (this as Person).ToEntity();
            return new ParentEntity(person);
        }

        public static Parent ToInstance(ParentEntity entity)
        {
            return new Parent(entity);
        }

        public void Update(Parent trustee)
        {
            this._name = trustee._name;
            this._schoolKidIds = trustee._schoolKidIds;
            this._imageId = trustee._imageId;
        }
    }
}
