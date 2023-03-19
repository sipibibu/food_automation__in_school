using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class FileModel : IInstance<FileModel.Entity>
{
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

    public class Entity : IInstance<Entity>.IEntity<FileModel>
    {
        public string Id;
        public string Name;
        public string Path;
        public FileModel ToInstance()
        {
            throw new NotImplementedException();
        }
    }

    public Entity ToEntity()
    {
        throw new NotImplementedException();
    }
}