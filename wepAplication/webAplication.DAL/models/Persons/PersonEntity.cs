using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public abstract class PersonEntity
    {
        [Key]
        public string Id { get; set; }
        public string? ImageId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public PersonEntity()
        {
        }

    }
}
