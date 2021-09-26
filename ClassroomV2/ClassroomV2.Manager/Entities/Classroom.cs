using ClassroomV2.DataAccessLayer.Entities;
using ClassroomV2.Membership.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClassroomV2.Manager.Entities
{
    public class Classroom : IEntity<int>
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string ClassroomName { get; set; }
        [Required, MaxLength(512)]
        public string Description { get; set; }
        [ForeignKey("ApplicationUser")]
        public Guid CreatorId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
