using webAplication.DAL.models;
using webAplication.DAL.models.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain.Persons
{
    public class SchoolKid : Person, ITransferredInstance<SchoolKidEntity, SchoolKid>
    {
        private SchoolKid(string role, string name) : base("SchoolKid", name) { }
        public SchoolKid(SchoolKidEntity entity) : base(entity) { }
        public SchoolKidEntity ToEntity()
        {
            var person = (this as Person).ToEntity();
            return new SchoolKidEntity(person);
        }
        public static SchoolKid ToInstance(SchoolKidEntity entity)
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
