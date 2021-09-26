using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.Repositories
{
    public interface IClassroomRepository : IRepository<Classroom, int>
    {
    }
}
