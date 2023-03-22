using JsonKnownTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Admin : Person, IInstance<Admin.Entity>
    {
        public new class Entity : Person.Entity, IInstance<Entity>.IEntity<Admin>
        {/*        
            [JsonProperty("n")]
            public int n { get; set; }*/
            public Entity() : base() { }
            public Entity(Admin admin) : base(admin)
            {
            }
            public new Admin ToInstance()
            {
                return new Admin(this);
            }
        }
        public Admin(string name,string login=null,string password=null) : base("admin", name) 
        {
            if(login != null & password!=null) {
                _user = new User.Entity()
                {
                    Id = _id,
                    Login = login,
                    Password = password,
                    Person = this.ToEntity()
                };
                _userId = _user.Id;
            }
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
