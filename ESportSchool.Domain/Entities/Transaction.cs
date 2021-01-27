using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public float Value { get; set; }
        
        //relations
        public User User { get; set; }
    }
}