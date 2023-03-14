using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public abstract class Person
    {
        private string _id = Guid.NewGuid().ToString();
        public string Id { get {  return _id; } set { } }
        public string? imageId { get; set; }
        public string name { get; set; }
        public string role { get; private set; }

        private Person()
        {
        }
        public abstract PersonEntity ToEntity(){}


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
