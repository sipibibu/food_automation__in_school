using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class FileModel : IInstance<FileModel.Entity>
{
    public class Entity : IInstance<Entity>.IEntity<FileModel>
    {
        public string Id;
        public string Name;
        public string Path;
        public FileModel ToInstance()
        {
            return new FileModel(this);
        }
    }
    private string _id;
    private string _name;
    private string _path;

    private FileModel() { throw new Exception(); }

    private FileModel(FileModel.Entity entity)
    {
        _id = entity.Id;
        _name = entity.Name;
        _path = entity.Path;
    }

    public Entity ToEntity()
    {
        throw new NotImplementedException();
    }
}