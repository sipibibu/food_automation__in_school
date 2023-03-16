using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class Admin : Person, ITransferredInstance<AdminEntity, Admin>
    {
        private Admin(string name) : base("Admin", name) { }
        private Admin(AdminEntity entity) : base(entity) { }
        public AdminEntity ToEntity()
        {
            var person = (this as Person).ToEntity();
            return new AdminEntity(person);
        }

        public static Admin ToInstance(AdminEntity entity)
        {
            return new Admin(entity);
        }
    }
}
