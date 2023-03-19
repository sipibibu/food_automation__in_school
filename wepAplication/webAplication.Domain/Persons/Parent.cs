using webAplication.DAL.models;
using webAplication.DAL.models.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Parent : Person, ITransferredInstance<ParentEntity, Parent>
    {
        private List<SchoolKid> _schoolKids = new List<SchoolKid>();
        public Parent(string name) : base("parent", name) { }

        private Parent(ParentEntity entity) : base(entity)
        {
            _schoolKids = entity.SchoolKids
                .Select(sc => SchoolKid.ToInstance(sc))
                .ToList();
        }

        public void AddSchoolKid(SchoolKid schoolKid)
        {
            _schoolKids.Add(schoolKid);
        }
        public void ReplaceSchoolKids(List<SchoolKid> schoolKids)
        {
            _schoolKids.Clear();
            _schoolKids = schoolKids;
        }

        public List<SchoolKidEntity> GetSchoolKidsEntities()
        {
            return _schoolKids
                .Select(x => x.ToEntity())
                .ToList();
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
            this._schoolKids = trustee._schoolKids;
            this._imageId = trustee._imageId;
        }
    }
}
