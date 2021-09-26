using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClassroomV2.Manager.Repositories
{
    public class StudentRepository : Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(IManagerContext context)
            : base((DbContext)context)
        {
        }
    }
}
