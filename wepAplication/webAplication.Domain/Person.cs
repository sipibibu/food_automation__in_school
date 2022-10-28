namespace webAplication.Models
{
    public class Person
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id { get {  return _id; } }
        public string name { get; private set; }

        //private readonly IRole _role;
        //private IRole Role { get { return _role; } }
    }
}
