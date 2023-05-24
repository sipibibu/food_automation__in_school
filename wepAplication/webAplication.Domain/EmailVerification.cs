using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using JsonKnownTypes;
using Newtonsoft.Json;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    [JsonConverter(typeof(JsonKnownTypesConverter<EmailVerification>))]
    [JsonKnownType(typeof(EmailVerification), "EmailVerification")]
    public class EmailVerification: IInstance<EmailVerification.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<EmailVerification>
        {
            [Key]
            public string VerificationToken { get; set; }
            public string UserId { get; set; }
            public string Email { get; set; }
            public long Date { get; set; }
            public EmailVerification ToInstance()
            {
                return new EmailVerification()
                {
                    verificationToken = VerificationToken,
                    userId = UserId,
                    email = Email,
                    date = Date
                };
        }
        }
        [JsonProperty("Token")]
        protected string verificationToken { get; set; }
        [JsonProperty("UserId")]
        protected string userId { get; set; }
        [JsonProperty("Email")]
        protected string email { get; set; }
        [JsonProperty("Date")]
        protected long date { get; set; }
       
        public EmailVerification(){}

        public Entity ToEntity()
        {
            return new Entity()
            {
                VerificationToken = verificationToken,
                UserId = userId,
                Email = email,
                Date = date
            };
        }

        
    }
}
