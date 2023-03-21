using System.ComponentModel.DataAnnotations;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class SchoolKidAttendance : IInstance<SchoolKidAttendance.Entity>
    {
        public class Entity : IInstance<Entity>.IEntity<SchoolKidAttendance>
        {
            [Key]
            public string SchoolKidId { get; set; }
            public SchoolKidAttendanceType Attendance { get; set; }
            public Entity() { }
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
            _schoolKidId = entity.SchoolKidId;
            _attendance = entity.Attendance;
        }

        public Entity ToEntity()
        {
            return new Entity()
            {
                SchoolKidId = _schoolKidId,
                Attendance = _attendance
            };
        }
    }
}
