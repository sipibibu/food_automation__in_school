namespace webAplication.DAL.models
{
    public class TrusteeEntity : PersonEntity
    {
        public TrusteeEntity() : base() {
            schoolKidIds = new List<string>();
        }
        public List<string> schoolKidIds;
    }
}
