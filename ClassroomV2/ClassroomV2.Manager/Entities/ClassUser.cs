using ClassroomV2.DataAccessLayer.Entities;
using ClassroomV2.Membership.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClassroomV2.Manager.Entities
{
    public class ClassUser : IEntity<int>
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string email { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}
