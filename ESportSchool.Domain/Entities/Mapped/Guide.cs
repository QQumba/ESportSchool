using ESportSchool.Domain.Constants;

namespace ESportSchool.Domain.Entities.Mapped
{
    public class Guide : BaseEntity
    {
        public string HtmlBody { get; set; }
        public Game Game { get; set; }
        //relations
        public virtual User User { get; set; }
    }
}