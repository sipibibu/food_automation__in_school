using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class UserEntity
    {
        [Key]
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PersonId { get; set; }
        public UserEntity() { }
    }
}