using ClassroomV2.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomV2.Manager.Entities
{
    public class Student : IEntity<int>
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string email { get; set; }
        [ForeignKey("Classroom")]
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
