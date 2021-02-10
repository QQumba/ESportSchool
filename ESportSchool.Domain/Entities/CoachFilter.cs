using System;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Domain.Entities
{
    public class CoachFilter
    {
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public Language Language { get; set; }
        public Game Game { get; set; }
    }
}