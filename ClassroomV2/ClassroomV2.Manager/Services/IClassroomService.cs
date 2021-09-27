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
        int CreateClassroom(Classroom classroom);
        (bool x, string message) JoinClassroom(int classId, string email);
        IList<Classroom> GetClasses(string mail);
        (bool isPermited, bool isTeacher, string Name, string Description, int Id) LoadClassRoomData(int classroomId, string mail);
        IList<Teacher> GetTeacherByClassId(int Id);
        IList<Student> GetStudentByClassId(int Id);
        (bool x, string message) AddTeacherToClass(int classId, string email);
    }
}
