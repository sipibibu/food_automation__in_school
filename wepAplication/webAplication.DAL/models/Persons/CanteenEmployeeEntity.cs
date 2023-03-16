namespace webAplication.DAL.models
{
    public class CanteenEmployeeEntity : PersonEntity
    {
        public CanteenEmployeeEntity() : base() { }
        public CanteenEmployeeEntity(PersonEntity personEntity) : base(personEntity) {}
    }
}
