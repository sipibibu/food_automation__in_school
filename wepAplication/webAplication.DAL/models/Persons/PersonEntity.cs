namespace webAplication.DAL.models
{
    public abstract class PersonEntity
    {
        public string id;
        public string? imageId;
        public string name;
        public string role;

        public PersonEntity()
        {
        }

        public PersonEntity(string role, string name)
        {
            this.name = name;
            this.role = role;
        }
    }
}
