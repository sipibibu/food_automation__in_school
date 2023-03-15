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
            return new TeacherEntity()
            {
                Id = Id,
                Name = this._name,
                Role = this._role,
                ImageId = this._imageId
            };
        }
        public static Teacher FromEntity(TeacherEntity entity)
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
