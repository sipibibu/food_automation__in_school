using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Teacher : Person
    {
        public Teacher(string role, string name) : base(role, name) { }
        public Teacher(TeacherEntity entity):base(entity) { }
        public override TeacherEntity ToEntity()
        {
            return new TeacherEntity()
            {
                Id = this.Id,
                Name = this.name,
                Role = this.role,
                ImageId = this.imageId
            };
        }
        public void Update(Teacher teacher)
        {
            this.name = teacher.name;
            this.imageId = teacher.imageId;
        }
    }
}
