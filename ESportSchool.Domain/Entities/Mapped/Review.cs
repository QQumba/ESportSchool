namespace ESportSchool.Domain.Entities.Mapped
{
    public class Review : BaseEntity
    {
        public string Value { get; set; }
        
        //relations
        public User User { get; set; }
    }
}