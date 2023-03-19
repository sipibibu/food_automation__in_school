using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Admin : Person, IInstance<Admin.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<Admin>
        {
            public Entity() : base() { }
            public Entity(Admin admin) : base(admin) {}
            public new Admin ToInstance()
            {
                return new Admin(this);
            }
        }
        public Admin(string name) : base("Admin", name) { }
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        public static Admin ToInstance(Entity entity)
        {
            return new Admin(entity);
        }
        private Admin(Entity entity) : base(entity) { }
    }
}
