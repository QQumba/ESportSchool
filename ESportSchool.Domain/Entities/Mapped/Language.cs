using System.Collections.Generic;

namespace ESportSchool.Domain.Entities.Mapped
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        //relations
        public List<Coach> Profiles { get; set; }
    }
}