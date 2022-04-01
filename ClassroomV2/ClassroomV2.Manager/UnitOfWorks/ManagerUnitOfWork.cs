using ClassroomV2.DataAccessLayer.UnitOfWorks;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.UnitOfWorks
{
    public class ManagerUnitOfWork : UnitOfWork, IManagerUnitOfWorks
    {
        public IClassroomRepository Classroom { get; private set; }

        public ITeacherRepository Teacher { get; private set; }

        public IStudentRepository Student { get; private set; }

        public IPostRepository Post { get; private set; }

        public IClassUserRepository ClassUser { get; private set; }

        public IMaterialRepository Material { get; private set; }
        public ManagerUnitOfWork(IManagerContext context,
            IClassroomRepository classroom,
            ITeacherRepository teacher,
            IStudentRepository student,
            IPostRepository post, 
            IClassUserRepository classUser,
            IMaterialRepository material
            ) : base((DbContext)context)
        {
            Classroom = classroom;
            Teacher = teacher;
            Student = student;
            Post = post;
            ClassUser = classUser;
            Material = material;
        }
    }
}
