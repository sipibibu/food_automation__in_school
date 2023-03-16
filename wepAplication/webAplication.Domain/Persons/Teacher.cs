using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Teacher : Person, ITransferredInstance<TeacherEntity, Teacher>
    {
        private Teacher(string role, string name) : base(role, name) { }
        public Teacher(TeacherEntity entity):base(entity) { }
        public TeacherEntity ToEntity()
        {
            var person = (this as Person).ToEntity();
            return new TeacherEntity(person);
        }
        public static Teacher ToInstance(TeacherEntity entity)
        {
            return new Teacher(entity);
        }
        private void Update(Teacher teacher)
        {
            this._name = teacher._name;
            this._imageId = teacher._imageId;
        }
    }
}
