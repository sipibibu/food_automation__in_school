using webAplication.Domain.Persons;
using webAplication.DAL.models;
using webAplication.DAL.models.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Class : ITransferredInstance<ClassEntity, Class>
    {
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

        private Class(ClassEntity entity)
        {
            id = entity.Id;
            title = entity.Title;
            teacherId = entity.TeacherId;
            schoolKidIds = entity.SchoolKidIds;
            schoolKids = new List<SchoolKid>();

            foreach (var schoolKid in entity.SchoolKids)
            {
                schoolKids.Add(SchoolKid.ToInstance(schoolKid));
            }
        }
        public static Class ToInstance(ClassEntity entity)
        {
            return new Class(entity);
        }
        public ClassEntity ToEntity()
        {
            return new ClassEntity()
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

