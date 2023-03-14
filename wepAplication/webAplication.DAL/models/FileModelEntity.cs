using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class FileModelEntity
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileModelEntity() { }
    }
}