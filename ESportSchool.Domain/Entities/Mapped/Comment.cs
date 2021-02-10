namespace ESportSchool.Domain.Entities.Mapped
{
    public class Comment : BaseEntity
    {
        public string Value { get; set; }
        
        //relations
        public Coach Coach { get; set; }
        public User Writer { get; set; }
    }
}