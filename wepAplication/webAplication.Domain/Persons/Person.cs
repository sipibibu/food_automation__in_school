using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Person : ITransferredInstance<PersonEntity, Person>
    {
        private string _id;
        protected string? _imageId;
        protected string _name;
        protected string _role;
        
        protected Person(string role, string name)
        {
            this._name = name;
            this._role = role;
        }
        protected Person(PersonEntity entity)
        {
            this._id = entity.Id;
            this._imageId = entity.ImageId;
            this._name = entity.Name;
            this._role = entity.Role;
        }
        private Person() { throw new Exception(); }
        public PersonEntity ToEntity()
        {
            throw new NotImplementedException();
        }
        public static Person FromEntity(PersonEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
