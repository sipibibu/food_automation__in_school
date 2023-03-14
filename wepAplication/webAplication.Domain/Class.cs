using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Domain.Persons;
using webAplication.DAL.models;

namespace webAplication.Domain
{
    public class Class
    {
        public string Id { get { return id; } set { } }
        private string id = Guid.NewGuid().ToString();
        public string title { get; set; }
        public string teacherId { get; set; }    
        public string[] schoolKidIds { get; set; }
        public List<SchoolKid> schoolKids { get; set; }

        private Class()
        {
        }
        public Class(ClassEntity entity)
        {
            id= entity.Id;
            title = entity.Title;
            teacherId = entity.TeacherId;
            schoolKidIds = entity.SchoolKidIds;
            schoolKids = new List<SchoolKid>();
            
            for(int i = 0; i < entity.SchoolKids.Count(); i++)
            {
                schoolKids.Add(new SchoolKid(entity.SchoolKids[i]));
            }
        }
        public ClassEntity toEntity()
        {
            var pizduki = new List<SchoolKidEntity>();

            for (int i = 0; i < this.schoolKids.Count(); i++)
            {
                pizduki.Add(this.schoolKids[i].ToEntity());
            }

            return new ClassEntity()
            {
                Id = id,
                Title = title,
                TeacherId = teacherId,
                SchoolKids = pizduki,
                SchoolKidIds=this.schoolKidIds
            };
        }
        public void Update(Class _class)
        {
            title = _class.title;
            teacherId = _class.teacherId;
            schoolKidIds = _class.schoolKidIds;
        }
    }
}
