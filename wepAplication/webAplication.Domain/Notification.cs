using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class Notification : IInstance<Notification.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<IInstance<Entity>>
        {
            [Key]
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public DateTime PublishedAt { get; set; }
            public IInstance<Entity> ToInstance()
            {
                throw new NotImplementedException();
            }
        }
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
        public Entity ToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
