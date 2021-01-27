using System;
using System.Collections.Generic;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    //TODO: add payment tracking 
    public class Training : BaseEntity 
    {
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public bool Accepted { get; set; }
        
        //relations
        public CoachProfile Coach { get; set; }
        public List<User> Participants { get; set; }
    }
}