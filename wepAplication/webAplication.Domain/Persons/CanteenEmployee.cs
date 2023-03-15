using webAplication.DAL.models;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person
    {
        private CanteenEmployee(string name) : base("canteenEmployee", name) { }
        public CanteenEmployee(CanteenEmployeeEntity entity) : base(entity) { }
        public override CanteenEmployeeEntity ToEntity()
        {
            return new CanteenEmployeeEntity()
            {
                Id = this.Id,
                Name = _name,
                Role = _role,
                ImageId = _imageId
            };
        }
        private void Update(CanteenEmployee canteenEmployee)
        {
            _imageId = canteenEmployee._imageId;
            _name = canteenEmployee._name;
        }
    }
}
