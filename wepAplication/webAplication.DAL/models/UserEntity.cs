using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    internal class UserEntity
    {
        [Key]
        public string Id;
        public string Login;
        public string Password;
        public string PersonId;
        public UserEntity() { }
    }
}