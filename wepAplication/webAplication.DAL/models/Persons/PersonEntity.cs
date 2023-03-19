using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public abstract class PersonEntity : Entity
    {
        [Key]
        public string Id { get; set; }
        public string? ImageId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public UserEntity User { get; set; }

        public PersonEntity()
        {
        }

        public PersonEntity(PersonEntity entity)
        {
            Id = entity.Id;
            ImageId = entity.Id;
            Name = entity.Name;
            Role = entity.Role;
        }
    }
}
