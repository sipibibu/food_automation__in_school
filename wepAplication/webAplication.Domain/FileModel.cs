using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class FileModel : ITransferredInstance<FileModelEntity, FileModel>
{
    private string _id;
    private string _name;
    private string _path;

    private FileModel() { throw new Exception(); }

    private FileModel(FileModelEntity entity)
    {
        _id = entity.Id;
        _name = entity.Name;
        _path = entity.Path;
    }
    public FileModelEntity ToEntity()
    {
        return new FileModelEntity()
        {
            Id = _id,
            Name = _name,
            Path = _path
        };
    }
    public static FileModel ToInstance(FileModelEntity entity)
    {
        return new FileModel(entity);
    }
}