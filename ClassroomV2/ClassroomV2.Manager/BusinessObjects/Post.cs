using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomV2.Manager.BusinessObjects
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClassroomId { get; set; }
        public string FilePath { get; set; }
        public DateTime PostCreatedTime { get; set; }
    }
}
