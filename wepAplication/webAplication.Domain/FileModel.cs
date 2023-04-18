using JsonKnownTypes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain;

[JsonConverter(typeof(JsonKnownTypesConverter<FileModel>))]
[JsonKnownType(typeof(FileModel), "FileModel")]
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
        private Entity():base() { }
        public Entity(FileModel fileModel)
        {
            Id = fileModel._id;
            Name = fileModel._name;
            Path = fileModel._path;
        }
    }
    [JsonProperty("Id")]
    private string _id;
    [JsonProperty("Name")]
    private string _name;
    [JsonProperty("Path")]
    private string _path;

    private FileModel() {}
    private FileModel(Entity entity)
    {
        _id = entity.Id;
        _name = entity.Name;
        _path = entity.Path;
    }

    public FileModel(IFormFile uploadedFile, string path)
    {
        _id=Guid.NewGuid().ToString();
        _name = uploadedFile.FileName;
        _path = path;
    }
    public Entity ToEntity()
    {
        return new Entity(this);
    }
}