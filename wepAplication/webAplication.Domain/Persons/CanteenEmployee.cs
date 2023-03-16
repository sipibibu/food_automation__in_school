using webAplication.DAL.Interfaces;
using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class CanteenEmployee : Person, ITransferredInstance<CanteenEmployeeEntity, CanteenEmployee>
    {
        private CanteenEmployee(string name) : base("canteenEmployee", name) { }
        private CanteenEmployee(CanteenEmployeeEntity entity) : base(entity) { }
        public CanteenEmployeeEntity ToEntity()
        {
            var person = (this as Person).ToEntity();
            return new CanteenEmployeeEntity(person);
        }

        public static CanteenEmployee ToInstance(CanteenEmployeeEntity entity)
        {
            return new CanteenEmployee(entity);
        }

        private void Update(CanteenEmployee canteenEmployee)
        {
            _imageId = canteenEmployee._imageId;
            _name = canteenEmployee._name;
        }
    }
}
