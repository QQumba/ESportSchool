using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string HtmlBody { get; set; }
        public string SourceUrl { get; set; }
        public string PostDate { get; set; }
    }
}