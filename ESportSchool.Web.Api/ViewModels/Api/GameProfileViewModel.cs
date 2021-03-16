using System.ComponentModel.DataAnnotations;
using ESportSchool.Domain.Constants;

namespace ESportSchool.Web.ViewModels.Api
{
    public class GameProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        public Game Game { get; set; }
        public string About { get; set; }
        public string Rank { get; set; }
        public string AdditionalRankInfo { get; set; }
    }
}