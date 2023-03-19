using webAplication.Domain.Persons;
using webAplication.Domain.Interfaces;

namespace webAplication.Domain
{
    public class SchoolKidAttendance : IInstance<SchoolKidAttendanceEntity>
    {
        private string _schoolKidId;

        private SchoolKidAttendanceType _attendance = SchoolKidAttendanceType.Unknown;
        public SchoolKidAttendanceType schoolKidAttendanceType { get { return _attendance; } set { _attendance = value; } }

        private SchoolKidAttendance() { throw new Exception(); }

        private SchoolKidAttendance(SchoolKidAttendanceEntity entity)
        {
            _schoolKidId = entity.SchoolKidId;
            _attendance = entity.Attendance;
        }

        public SchoolKidAttendanceEntity ToEntity()
        {
            return new SchoolKidAttendanceEntity()
            {
                SchoolKidId = _schoolKidId,
                Attendance = _attendance
            };
        }
        public static SchoolKidAttendance ToInstance(SchoolKidAttendanceEntity entity)
        {
            return new SchoolKidAttendance(entity);
        }
    }
}
