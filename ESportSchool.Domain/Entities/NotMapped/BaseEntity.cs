using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESportSchool.Domain.Entities.NotMapped
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime LastUpdateTimestamp { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}