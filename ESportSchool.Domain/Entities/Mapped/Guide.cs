namespace ESportSchool.Domain.Entities.Mapped
{
    public class Guide : BaseEntity
    {
        public string HtmlBody { get; set; }
        
        //relations
        public User User { get; set; }
    }
}