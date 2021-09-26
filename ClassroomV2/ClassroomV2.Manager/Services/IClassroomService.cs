using ClassroomV2.Manager.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.Services
{
    public interface IClassroomService
    {
        void CreateClassroom(Classroom classroom);
    }
}
