using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class FileModel : IInstance<FileModelEntity>
{
    public string Id { get { return id; } set { } }
    private string id = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Path { get; set; }
    public FileModelEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public static IInstance<FileModelEntity> FromEntity()
    {
        throw new NotImplementedException();
    }
}