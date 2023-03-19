using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person, IInstance<CanteenEmployee.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<CanteenEmployee>
        {
            public Entity() : base() { }
            public Entity(CanteenEmployee canteenEmployee) : base(canteenEmployee) {}
            public new CanteenEmployee ToInstance()
            {
                return new CanteenEmployee(this);
            }
        }
        public CanteenEmployee(string name) : base("canteenEmployee", name) { }
        private CanteenEmployee(Entity entity) : base(entity) { }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        public static CanteenEmployee ToInstance(Entity entity)
        {
            return new CanteenEmployee(entity);
        }
        private void Update(CanteenEmployee canteenEmployee)
        {
            _imageId = canteenEmployee._imageId;
            _name = canteenEmployee._name;
        }
    }
}
