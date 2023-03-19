using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class UserEntity : Entity
    {
        [Key]
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public PersonEntity Person { get; set; }
        public UserEntity() { }
    }
}