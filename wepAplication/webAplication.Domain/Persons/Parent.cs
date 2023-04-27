using Newtonsoft.Json;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Parent : Person, IInstance<Parent.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Parent.Entity>.IEntity<Parent>
        {
            public List<SchoolKid.Entity> SchoolKids { get; }  = new List<SchoolKid.Entity>();
            public Entity() : base() { }

            internal Entity(Parent parent) : base(parent)
            {
                SchoolKids = parent.SchoolKids?
                    .Select(sc => sc.ToEntity())
                    .ToList();
            }

            public void Update(Parent.Entity parent)
            {
                this.SchoolKids.Clear();
                this.SchoolKids.AddRange(parent.SchoolKids);
                this.Name = parent.Name;
                this.Role = parent.Role;
                this.User = parent.User;
                this.UserId = parent.UserId;
                this.ImageId = parent.ImageId;
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
        public List<SchoolKid> SchoolKids { get; set; }

        [JsonProperty("SchoolKidIds")] 
        public List<string> SchoolKidIds { get; set; }
        
        public Parent(string name) : base("parent", name) { }
        private Parent(Entity entity) : base(entity)
        {
            SchoolKids = entity.SchoolKids
                .Select(sc => sc.ToInstance())
                .ToList();
        }
        public void AddSchoolKid(SchoolKid schoolKid)
        {
            SchoolKids.Add(schoolKid);
        }
        public void ReplaceSchoolKids(IEnumerable<SchoolKid> schoolKids)
        {
            SchoolKids.Clear();
            SchoolKids.AddRange(
                schoolKids
                    .Where(x => x != null)
                    .ToList()!);
            // SchoolKids
            //     .ForEach(x => x.SetParent(this));
        }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
    }
}
