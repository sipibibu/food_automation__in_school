using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Service.Models
{
    internal class Notification
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
