using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class Admin : Person
    {
        public Admin(string name) : base("Admin", name) { }
        private Admin(AdminEntity entity) : base(entity) { }

        public override AdminEntity ToEntity()
        {
            return new AdminEntity()
            {
                Id = this.Id,
                Name = this.name,
                Role = this.role,
                ImageId = this.imageId
            };
        }
        public static Admin FromEntity(AdminEntity entity)
        {
            return new Admin(entity);
        }

/*        public override Person FromEntity()
        {
            throw new NotImplementedException();
        }*/
    }
}
