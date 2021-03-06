using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Context;
using ClassroomV2.Manager.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.Repositories
{
    public class ClassroomRepository : Repository<Classroom, int>, IClassroomRepository
    {
        public ClassroomRepository(IManagerContext context)
            : base((DbContext)context)
        {
        }
    }
}
