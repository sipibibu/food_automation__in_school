using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

public class FileModel : ITransferredInstance<FileModelEntity, FileModel>
{
    private string _id;
    public string Name { get; set; }
    public string Path { get; set; }
    public FileModelEntity ToEntity()
    {
        throw new NotImplementedException();
    }

    public static FileModel FromEntity(FileModelEntity entity)
    {
        throw new NotImplementedException();
    }
}