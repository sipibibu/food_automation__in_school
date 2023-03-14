namespace webAplication.DAL.models
{
    public class ParentEntity : PersonEntity
    {
        public ParentEntity() : base() {
            schoolKidIds = new List<string>();
        }
        public List<string> schoolKidIds;
    }
}
