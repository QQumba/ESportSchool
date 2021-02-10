using System.Collections.Generic;

namespace ESportSchool.Domain.Entities.Mapped
{
    public class Team : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        
        //relations
        public virtual List<User> Users { get; set; }
    }
}