using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class NotificationEntity
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
