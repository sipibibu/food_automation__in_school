using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Security.Claims;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public abstract class Person : IInstance<Person.Entity>
    {
        public abstract class Entity : IInstance<Person.Entity>.IEntity<Person>
        {
            [Key]
            public string Id { get; set; }
            public string? ImageId { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public User.Entity User { get; set; }
            public string UserId { get; set; }

            public Entity()
            {
            }

            public Entity(Person person)
            {
                Id = person._id;
                ImageId = person._imageId;
                Name = person._name;
                Role = person._role;
                User = person._user;
                UserId = person._userId;
            }

            public Person ToInstance()
            {
                throw new NotImplementedException();
            }
            public dynamic GetPerson()
            {
                if (this is Admin.Entity admin)
                    return admin;
                if (this is CanteenEmployee.Entity canteenEmployee)
                    return canteenEmployee;
                if (this is Teacher.Entity teacher)
                    return teacher;
                if (this is Parent.Entity parent)
                    return parent;
                if (this is SchoolKid.Entity schoolKid)
                    return schoolKid;
                return null;
            }
        }
        
        private string _id;
        protected string? _imageId;
        protected string _name;
        protected string _role;
        protected User.Entity _user;
        protected string _userId;
        
        protected Person(string role, string name)
        {
            _id = Guid.NewGuid().ToString();
            _name = name;
            _role = role; 
        }
        protected Person(Entity entity)
        {
            _id = entity.Id;
            _imageId = entity.ImageId;
            _name = entity.Name;
            _role = entity.Role;
            _userId = entity.UserId;
            _user = entity.User;
        }
        private Person() { throw new Exception(); }
        public dynamic GetPerson()
        {
            if (this is Admin admin)
                return admin;
            if (this is CanteenEmployee canteenEmployee)
                return canteenEmployee;
            if (this is Teacher teacher)
                return teacher;
            if (this is Parent parent)
                return parent;
            if (this is SchoolKid schoolKid)
                return schoolKid;
            return null;
        }
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
