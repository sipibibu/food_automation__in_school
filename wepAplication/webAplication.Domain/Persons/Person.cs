using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Person : IInstance<Person.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<Person>
        {
            [Key]
            public string Id { get; set; }
            public string? ImageId { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public User.Entity User { get; set; }

            public Entity()
            {
            }

            public Entity(Person person)
            {
                Id = person._id;
                ImageId = person._imageId;
                Name = person._name;
                Role = person._role;
            }
            public Person ToInstance()
            {
                throw new NotImplementedException();
            }
        }
        
        private string _id;
        protected string? _imageId;
        protected string _name;
        protected string _role;
        
        protected Person(string role, string name)
        {
            _id = Guid.NewGuid().ToString();
            _name = name;
            _role = role;
        }
        protected Person(Entity entity)
        {
            this._id = entity.Id;
            this._imageId = entity.ImageId;
            this._name = entity.Name;
            this._role = entity.Role;
        }
        private Person() { throw new Exception(); }
        public Claim GetClaim()
        {
            return new Claim("role", _role);
        }
        public Entity ToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
