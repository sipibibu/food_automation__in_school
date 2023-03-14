using System.Diagnostics.Tracing;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public /*abstract*/ class Person:IInstance<PersonEntity>
    {
        private string _id = Guid.NewGuid().ToString();
        public string Id { get {  return _id; } set { } }
        public string? imageId { get; set; }
        public string name { get; set; }
        public string role { get; private set; }

        private Person()
        {
        }
        public virtual PersonEntity ToEntity() { throw new NotImplementedException(); }
        public static Person FromEntity() { throw new NotImplementedException(); }


        public Person(string role, string name)
        {
            this.name = name;
            this.role = role;
        }
        public Person(PersonEntity entity)
        {
            this._id = entity.Id;
            this.imageId = entity.ImageId;
            this.name = entity.Name;
            this.role = entity.Role;
        }
    }
}
