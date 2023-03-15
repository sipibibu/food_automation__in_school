using webAplication.DAL.models;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person, ITransferred<SchoolKidEntity, SchoolKid>
    {
        private SchoolKid(string role, string name) : base("SchoolKid", name) { }
        public SchoolKid(SchoolKidEntity entity) : base(entity) { }
        public SchoolKidEntity ToEntity()
        {
            return new SchoolKidEntity()
            {
                Id = Id,
                Name = _name,
                Role = _role,
                ImageId = _imageId
            };
        }
        public static SchoolKid FromEntity(SchoolKidEntity entity)
        {
            return new SchoolKid(entity);
        }
        private void Update(SchoolKid schoolKid)
        {
            this._name = schoolKid._name;
            this._imageId = schoolKid._imageId;
        }
    }
}
