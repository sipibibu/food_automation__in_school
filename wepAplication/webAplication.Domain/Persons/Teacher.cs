using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Teacher : Person, IInstance<Teacher.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<Teacher>
        {
            public Entity() : base() { }
            public Entity(Teacher teacher) : base(teacher) {}
            public new Teacher ToInstance()
            {
                return new Teacher(this);
            }
        }
        public Teacher(string name) : base("teacher", name) { }
        private Teacher(Entity entity):base(entity) { }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        public static Teacher ToInstance(Entity entity)
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
