using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person, IInstance<SchoolKid.Entity>
    {
        public new class Entity : Person.Entity, IInstance<SchoolKid.Entity>.IEntity<SchoolKid>
        {
            public string ClassId { get; set; }
            public Class.Entity _Class { get; set; }
            public Entity() : base(){ }
            public Entity(SchoolKid schoolKid) : base(schoolKid) 
            {
                ClassId = schoolKid.classId;
                _Class = schoolKid._class?.ToEntity();
            }
            public new SchoolKid ToInstance()
            {
                return new SchoolKid(this);
            }
            public override string ToString()
            {
                return GetType().ToString();
            }
        }
        protected Class _class { get; set; }
        public string classId { get; set; }

        public SchoolKid(string name) : base("schoolKid", name) { }
        private SchoolKid(Entity entity) : base(entity) 
        {
            classId = entity.ClassId;
            _class = entity._Class?.ToInstance();
        }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        private void Update(SchoolKid schoolKid)
        {
            this._name = schoolKid._name;
            this._imageId = schoolKid._imageId;
        }
    }
}
