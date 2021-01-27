using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Value { get; set; }
        
        //relations
        public CoachProfile Coach { get; set; }
        public User Writer { get; set; }
    }
}