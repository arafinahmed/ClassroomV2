using System;

namespace ClassroomV2.Manager.BusinessObjects
{
    public class Classroom
    {
        public int Id { get; set; }
        public string ClassroomName { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public string Email { get; set; }
    }
}
