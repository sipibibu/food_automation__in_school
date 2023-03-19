using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person, IInstance<SchoolKid.Entity>
    {
        public new class Entity : Person.Entity, IInstance<SchoolKid.Entity>.IEntity<SchoolKid>
        {
            public Entity():base(){ }
            public Entity(SchoolKid schoolKid) : base(schoolKid) {}
            public new SchoolKid ToInstance()
            {
                throw new NotImplementedException();
            }
        }
        private SchoolKid(string role, string name) : base("schoolKid", name) { }
        public SchoolKid(Entity entity) : base(entity) { }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        public static SchoolKid ToInstance(Entity entity)
        {
            return new SchoolKid(entity);
        }
        private void Update(SchoolKid schoolKid)
        {
            this._name = schoolKid._name;
            this._imageId = schoolKid._imageId;
        }
    }
}
