using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Teacher : Person
    {
        public Teacher(string role, string name) : base(role, name) { }

        public void Update(Teacher teacher)
        {
            this.name = teacher.name;
            this.imageId = teacher.imageId;
        }
    }
}
