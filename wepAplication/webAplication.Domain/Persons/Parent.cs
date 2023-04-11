using Newtonsoft.Json;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Parent : Person, IInstance<Parent.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Parent.Entity>.IEntity<Parent>
        {
            public List<SchoolKid.Entity> SchoolKids = new List<SchoolKid.Entity>();
            public Entity() : base() { }

            public Entity(Parent parent) : base(parent)
            {
                SchoolKids = parent._schoolKids
                    .Select(sc => sc.ToEntity())
                    .ToList();
            }
            public new Parent ToInstance()
            {
                return new Parent(this);
            }
            public override string ToString()
            {
                return GetType().ToString();
            }
        }
        [JsonProperty("SchoolKids")]
        private List<SchoolKid> _schoolKids = new List<SchoolKid>();
        public Parent(string name) : base("parent", name) { }
        private Parent(Entity entity) : base(entity)
        {
            _schoolKids = entity.SchoolKids
                .Select(sc => sc.ToInstance())
                .ToList();
        }
        public void AddSchoolKid(SchoolKid schoolKid)
        {
            _schoolKids.Add(schoolKid);
        }
        public void ReplaceSchoolKids(List<SchoolKid?> schoolKids)
        {
            _schoolKids.Clear();
            _schoolKids = schoolKids
                .Where(x => x != null)
                .ToList();
            _schoolKids
                .ForEach(x => x.SetParent(this));
        }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        public void Update(Parent trustee)
        {
            this._name = trustee._name;
            this._schoolKids = trustee._schoolKids;
            this._imageId = trustee._imageId;
        }
    }
}
