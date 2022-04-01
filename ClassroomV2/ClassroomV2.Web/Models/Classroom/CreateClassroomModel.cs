using Autofac;
using ClassroomV2.Manager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomV2.Web.Models.Classroom
{
    public class CreateClassroomModel
    {
        public int Id { get; set; }
        [Required]
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public Guid AspUserId { get; set; }
        public string email { get; set; }

        public bool JoinCalled { get; set; }
        public bool joinSuccess { get; set; }
        public string joinMessage { get; set; }

        private IClassroomService _service;
        private ILifetimeScope _scope;
        public CreateClassroomModel() { }
        public CreateClassroomModel(IClassroomService service)
        {
            _service = service;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<IClassroomService>();
        }

        internal int CreateClassroom()
        {
            return _service.CreateClassroom(new ClassroomV2.Manager.BusinessObjects.Classroom
            {
                ClassroomName = ClassroomName,
                Description = Description,
                CreatorId = AspUserId,
                Email = email
            });
        }

        internal int CloneClassroom()
        {
            return _service.CloneClassroom(new ClassroomV2.Manager.BusinessObjects.Classroom
            {
                Id = Id,
                ClassroomName = ClassroomName,
                Description = Description,
                CreatorId = AspUserId,
                Email = email
            });
        }
        internal void JoinClassroom(int id, string mail)
        {
            var res =  _service.JoinClassroom(id, mail);
            JoinCalled = true;
            joinMessage = res.message;
            joinSuccess = res.x;
        }
        internal object GetClasses(string mail)
        {
            var data = _service.GetClasses(mail);
            return new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = (from record in data
                        select new string[]
                        {
                            record.ClassroomName,
                            record.Id.ToString()
                        }).ToArray()
            };
        }
    }
}
