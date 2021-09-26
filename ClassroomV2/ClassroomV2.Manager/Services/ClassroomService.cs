using AutoMapper;
using ClassroomV2.Manager.BusinessObjects;
using ClassroomV2.Manager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IManagerUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;
        public ClassroomService(IManagerUnitOfWorks unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void CreateClassroom(Classroom classroom)
        {
            if (classroom == null)
                throw new InvalidOperationException("no Classroom is provided");
            try
            {
                var entity = _mapper.Map<Entities.Classroom>(classroom);
                _unitOfWork.Classroom.Add(entity);
                _unitOfWork.Save();
                _unitOfWork.ClassUser.Add(new Entities.ClassUser { ClassId = entity.Id, email = classroom.Email });
                _unitOfWork.Save();
            }
            catch
            {
                throw new Exception("Entity not added");
            }

        }
    }
}
