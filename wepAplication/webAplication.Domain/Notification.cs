using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Service.Models
{
    internal class Notification : IInstance<NotificationEntity>
    {
        public Notification()
        {
            Id = Guid.NewGuid().ToString();
            PublishedAt = DateTime.Now;
        }
        public readonly string Id;
        public readonly string Title;
        public readonly string Description;
        public readonly string Type;
        public readonly DateTime PublishedAt;
    }
}
