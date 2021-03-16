namespace ESportSchool.Web.ViewModels.Api
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlBody { get; set; }
        public string SourceUrl { get; set; }
        public string PostDate { get; set; }
    }
}