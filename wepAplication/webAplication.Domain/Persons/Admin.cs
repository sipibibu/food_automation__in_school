using webAplication.DAL.models;


namespace webAplication.Domain.Persons
{
    public class Admin : Person
    {
        public Admin(string role, string name) : base(role, name) { }

        public Admin fromEntity(AdminEntity entity)
        {
            return new Admin(entity);
        }

    }
}
