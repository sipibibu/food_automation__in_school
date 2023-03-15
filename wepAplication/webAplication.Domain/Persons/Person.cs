using System.Diagnostics.Tracing;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public abstract class Person : IInstance, ITransferred<PersonEntity, Person>
    {
        public readonly string Id;
        protected string? _imageId;
        protected string _name;
        protected string _role;

        public virtual PersonEntity ToEntity() { throw new NotImplementedException(); }

        public static Person FromEntity(PersonEntity entity)
        {
            throw new NotImplementedException();
        }

        protected Person(string role, string name)
        {
            this._name = name;
            this._role = role;
        }

        protected Person(PersonEntity entity)
        {
            this.Id = entity.Id;
            this._imageId = entity.ImageId;
            this._name = entity.Name;
            this._role = entity.Role;
        }

        private Person()
        {
            throw new Exception();
        }
    }
}
