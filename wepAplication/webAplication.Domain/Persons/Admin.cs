using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Admin : Person, IInstance<Admin.Entity>
    {
        private int n;
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<Admin>
        {
            public int n { get; set; }
            public Entity() : base() { }
            public Entity(Admin admin) : base(admin)
            {
                n = admin.n;
            }
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
        private Admin(Entity entity) : base(entity)
        {
            n= entity.n;
        }
    }
}
