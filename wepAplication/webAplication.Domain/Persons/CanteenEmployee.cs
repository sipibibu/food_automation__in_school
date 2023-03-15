using webAplication.DAL.Interfaces;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person, ITransferredInstance<CanteenEmployeeEntity, CanteenEmployee>
    {
        private CanteenEmployee(string name) : base("canteenEmployee", name) { }
        public CanteenEmployee(CanteenEmployeeEntity entity) : base(entity) { }
        public CanteenEmployeeEntity ToEntity()
        {
            return new CanteenEmployeeEntity()
            {
                Id = this.Id,
                Name = _name,
                Role = _role,
                ImageId = _imageId
            };
        }

        public static CanteenEmployee FromEntity(CanteenEmployeeEntity entity)
        {
            throw new NotImplementedException();
        }

        private void Update(CanteenEmployee canteenEmployee)
        {
            _imageId = canteenEmployee._imageId;
            _name = canteenEmployee._name;
        }
    }
}
