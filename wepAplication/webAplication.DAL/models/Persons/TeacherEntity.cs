namespace webAplication.DAL.models
{
    public class TeacherEntity : PersonEntity
    {
        public TeacherEntity() : base() { }
        public TeacherEntity(PersonEntity personEntity) : base(personEntity) {}
    }
}
