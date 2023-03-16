using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Notification : ITransferredInstance<NotificationEntity, Notification>
    {
        public readonly string Id;

        public readonly string? Type;
        
        public readonly string? Title;

        public readonly string? Description;

        public readonly DateTime PublishedAt;
        private Notification(NotificationEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Title = entity.Title;
            Description = entity.Description;
            PublishedAt = entity.PublishedAt;
        }
        public NotificationEntity ToEntity()
        {
            return new NotificationEntity()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                Type = Type,
                PublishedAt = PublishedAt
            };
        }
        public static Notification FromEntity(NotificationEntity entity)
        {
            return new Notification(entity);
        }
    }
}
