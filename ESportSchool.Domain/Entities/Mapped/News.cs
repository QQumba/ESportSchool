namespace ESportSchool.Domain.Entities.Mapped
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string HtmlBody { get; set; }
        public string SourceUrl { get; set; }
        public string PostDate { get; set; }
    }
}