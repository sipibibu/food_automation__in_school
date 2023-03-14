using System.ComponentModel.DataAnnotations;

namespace webAplication.DAL.models
{
    public class FileModelEntity
    {
        [Key]
        public string id;
        public string name;
        public string path;
        public FileModelEntity() { }
    }
}