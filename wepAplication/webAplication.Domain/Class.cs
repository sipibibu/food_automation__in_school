using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Class : IInstance<Class.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Class>
        {
            [Key]
            public string Id { get; set; }
            public string Title { get; set; }
            public string TeacherId { get; set; }
            public string[] SchoolKidIds { get; set; }
            public List<SchoolKid.Entity> SchoolKids { get; set; }
            public Class ToInstance()
            {
                return new Class(this);
            }
        }
        private string id;
        private string title;
        private string teacherId;
        private string[] schoolKidIds;
        private List<SchoolKid> schoolKids;

        private Class()
        {
            throw new Exception();
        }
        public Class(string title, string teacherId, string[] schoolKidIds, List<SchoolKid> schoolKids)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.teacherId = teacherId;
            this.schoolKidIds = schoolKidIds;
            this.schoolKids = schoolKids;
        }

        private Class(Entity entity)
        {
            id = entity.Id;
            title = entity.Title;
            teacherId = entity.TeacherId;
            schoolKidIds = entity.SchoolKidIds;
            schoolKids = new List<SchoolKid>();

            foreach (var schoolKid in entity.SchoolKids)
            {
                schoolKids.Add(schoolKid.ToInstance());
            }
        }
        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = id,
                Title = title,
                TeacherId = teacherId,
                SchoolKids = schoolKids.Select(x => x.ToEntity()).ToList(),
                SchoolKidIds = schoolKidIds,
            };
        }

        public void Update(Class _class)
        {
            title = _class.title;
            teacherId = _class.teacherId;
            schoolKidIds = _class.schoolKidIds;
        }
    }
}

