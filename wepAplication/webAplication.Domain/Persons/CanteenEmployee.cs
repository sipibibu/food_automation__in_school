using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person
    {
        public CanteenEmployee(string role, string name) : base(role, name) { }

        public void Update(CanteenEmployee canteenEmployee)
        {
            this.imageId = canteenEmployee.imageId;
            this.name = canteenEmployee.name;
        }
    }
}
