using System.ComponentModel.DataAnnotations;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL.models
{
    public class FileModelEntity : Entity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileModelEntity() { }
    }
}