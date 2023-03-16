namespace webAplication.DAL.models.Persons
{
    public class ParentEntity : PersonEntity
    {
        public ParentEntity() : base() {
            schoolKidIds = new List<string>();
        }
        public List<string> schoolKidIds;
        public ParentEntity(PersonEntity personEntity) : base(personEntity) {}

    }
}
