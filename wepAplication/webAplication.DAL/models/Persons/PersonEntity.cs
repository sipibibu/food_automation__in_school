namespace webAplication.DAL.models
{
    public abstract class PersonEntity
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public PersonEntity()
        {
        }

        public PersonEntity(string role, string name)
        {
            this.Name = name;
            this.Role = role;
        }
    }
}
