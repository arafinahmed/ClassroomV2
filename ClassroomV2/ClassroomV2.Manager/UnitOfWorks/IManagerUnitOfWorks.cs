using ClassroomV2.DataAccessLayer.UnitOfWorks;
using ClassroomV2.Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.UnitOfWorks
{
    public interface IManagerUnitOfWorks : IUnitOfWork
    {
        IClassroomRepository Classroom { get; }
        ITeacherRepository Teacher { get; }
        IStudentRepository Student { get; }
        IPostRepository Post { get; }
        IClassUserRepository ClassUser { get; }
        IMaterialRepository Material { get; }
    }
}
