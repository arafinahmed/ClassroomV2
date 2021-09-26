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
        [Required]
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public Guid AspUserId { get; set; }
        public string email { get; set; }

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

        internal void CreateClassroom()
        {
            _service.CreateClassroom(new ClassroomV2.Manager.BusinessObjects.Classroom
            {
                ClassroomName = ClassroomName,
                Description = Description,
                CreatorId = AspUserId,
                Email = email
            });
        }
    }
}
