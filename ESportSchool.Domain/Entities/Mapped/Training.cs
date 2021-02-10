using System;
using System.Collections.Generic;

namespace ESportSchool.Domain.Entities.Mapped
{
    //TODO: add payment tracking 
    public class Training : BaseEntity 
    {
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public bool Accepted { get; set; }
        
        //relations
        public Coach Coach { get; set; }
        public List<User> Participants { get; set; }
    }
}