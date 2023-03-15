using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Admin : Person
    {
        private Admin(string name) : base("Admin", name) { }
        private Admin(AdminEntity entity) : base(entity) { }

        public override AdminEntity ToEntity()
        {
            return new AdminEntity()
            {
                Id = Id,
                Name = _name,
                Role = _role,
                ImageId = _imageId
            };
        }
        public static Admin FromEntity(AdminEntity entity)
        {
            return new Admin(entity);
        }
    }
}
