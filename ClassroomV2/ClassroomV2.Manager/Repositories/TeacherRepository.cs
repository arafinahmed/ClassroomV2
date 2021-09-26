using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClassroomV2.Manager.Repositories
{
    public class TeacherRepository : Repository<Teacher, int>, ITeacherRepository
    {
        public TeacherRepository(IManagerContext context)
            : base((DbContext)context)
        {
        }
    }
}
