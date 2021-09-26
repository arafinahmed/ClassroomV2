using ClassroomV2.DataAccessLayer.Repositories;
using ClassroomV2.Manager.Entities;


namespace ClassroomV2.Manager.Repositories
{
    public interface IStudentRepository : IRepository<Student, int>
    {
    }
}
