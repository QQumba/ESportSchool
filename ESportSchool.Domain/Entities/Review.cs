using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class Review : BaseEntity
    {
        public string Value { get; set; }
        
        //relations
        public User User { get; set; }
    }
}