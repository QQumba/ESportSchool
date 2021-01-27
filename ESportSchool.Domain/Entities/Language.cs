using System.Collections.Generic;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        //relations
        public List<CoachProfile> Profiles { get; set; }
    }
}