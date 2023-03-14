using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person
    {
        public CanteenEmployee(string name) : base("canteenEmployee", name) { }
        public CanteenEmployee(CanteenEmployeeEntity entity) : base(entity) { }
        public override CanteenEmployeeEntity ToEntity()
        {
            return new CanteenEmployeeEntity()
            {
                Id = this.Id,
                Name = this.name,
                Role = this.role,
                ImageId = this.imageId
            };
        }
        public void Update(CanteenEmployee canteenEmployee)
        {
            this.imageId = canteenEmployee.imageId;
            this.name = canteenEmployee.name;
        }
    }
}
