using System;

namespace ClassroomV2.Manager.BusinessObjects
{
    public class Material
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClassroomId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public DateTime PostCreatedTime { get; set; }
    }
}
