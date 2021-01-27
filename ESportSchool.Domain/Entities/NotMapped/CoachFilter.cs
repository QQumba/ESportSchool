using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using ESportSchool.Domain.Constants;

namespace ESportSchool.Domain.Entities.NotMapped
{
    public class CoachFilter
    {
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public Language Language { get; set; }
        public Game Game { get; set; }
    }
}