using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class UserEntity : IEntity
    {
        [Key]
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PersonId { get; set; }
        public UserEntity() { }
    }
}