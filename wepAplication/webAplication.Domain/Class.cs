using webAplication.Domain.Persons;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Class : ITransferredInstance<ClassEntity, Class>
    {
        public string Id
        {
            get { return id; }
            set { }
        }

        private string id = Guid.NewGuid().ToString();
        public string title { get; set; }
        public string teacherId { get; set; }
        public string[] schoolKidIds { get; set; }
        public List<SchoolKid> schoolKids { get; set; }

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

            private Class(ClassEntity entity)
        {
            id = entity.Id;
            title = entity.Title;
            teacherId = entity.TeacherId;
            schoolKidIds = entity.SchoolKidIds;
            schoolKids = new List<SchoolKid>();

            for (int i = 0; i < entity.SchoolKids.Count(); i++)
            {
                schoolKids.Add(new SchoolKid(entity.SchoolKids[i]));
            }
        }
        public static Class FromEntity(ClassEntity entity)
        {
            return new Class(entity);
        }

        public ClassEntity ToEntity()
        {
            var pizduki = new List<SchoolKidEntity>();

            for (int i = 0; i < this.schoolKids.Count(); i++)
            {
                pizduki.Add(this.schoolKids[i].ToEntity());
            }

            return new ClassEntity()
            {
                Id = id,
                Title = title,
                TeacherId = teacherId,
                SchoolKids = pizduki,
                SchoolKidIds = this.schoolKidIds
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

