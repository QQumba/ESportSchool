namespace ESportSchool.Domain.Entities.Mapped
{
    public class Transaction : BaseEntity
    {
        public float Value { get; set; }
        
        //relations
        public User User { get; set; }
    }
}