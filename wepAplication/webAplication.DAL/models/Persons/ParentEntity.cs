namespace webAplication.DAL.models.Persons
{
    public class ParentEntity : PersonEntity
    {
        public List<SchoolKidEntity> SchoolKids = new List<SchoolKidEntity>();
        public ParentEntity() : base() { }
        public ParentEntity(PersonEntity personEntity) : base(personEntity) {}

    }
}
