namespace webAplication.DAL.models.Persons
{
    public class ParentEntity : PersonEntity
    {
        public List<string> SchoolKidIds = new List<string>();
        public ParentEntity() : base() { }
        public ParentEntity(PersonEntity personEntity) : base(personEntity) {}

    }
}
