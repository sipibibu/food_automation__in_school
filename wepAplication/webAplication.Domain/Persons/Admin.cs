using JsonKnownTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Admin : Person, IInstance<Admin.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<Admin>
        {
            public Entity(Admin admin) : base(admin)
            {
            }
            public new Admin ToInstance()
            {
                return new Admin(this);
            }
        }
        public Admin(string name) : base("admin", name) 
        {
        } 
        public new Entity ToEntity()
        {
            return new Entity(this);
        }
        private Admin(Entity entity) : base(entity)
        {
        }
    }
}
