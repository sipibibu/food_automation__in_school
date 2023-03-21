using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webAplication.Domain.Interfaces;
using webAplication.Domain.Persons;

namespace webAplication.Domain
{
    public class SchoolKidAttendance : IInstance<SchoolKidAttendance.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<SchoolKidAttendance>
        {
            [Key]
            [ForeignKey("SchoolKid")]
            public string Id { get; set; }
            public SchoolKidAttendanceType Attendance { get; set; }

            public Entity() { }
            public Entity(SchoolKid.Entity entity)
            {
                this.Id = entity.Id;
                this.Attendance = SchoolKidAttendanceType.Unknown;
            }
            public SchoolKidAttendance ToInstance()
            {
                return new SchoolKidAttendance(this);
            }
        }
        public enum SchoolKidAttendanceType
        {
            Unknown,
            Missing,
            Present
        }
        private string _schoolKidId;

        private SchoolKidAttendanceType _attendance = SchoolKidAttendanceType.Unknown;
        public SchoolKidAttendanceType schoolKidAttendanceType { get { return _attendance; } set { _attendance = value; } }

        private SchoolKidAttendance() { throw new Exception(); }

        private SchoolKidAttendance(Entity entity)
        {
            _schoolKidId = entity.Id;
            _attendance = entity.Attendance;
        }

        public Entity ToEntity()
        {
            return new Entity()
            {
                Id = _schoolKidId,
                Attendance = _attendance
            };
        }
    }
}
