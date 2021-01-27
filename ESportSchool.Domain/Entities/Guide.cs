using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class Guide : BaseEntity
    {
        public string HtmlBody { get; set; }
        
        //relations
        public User User { get; set; }
    }
}