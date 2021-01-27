using System.Collections.Generic;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.GameProfiles;

namespace ESportSchool.Web.ViewModels
{
    public class CoachProfileViewModel
    {
        public CsgoProfile CsgoProfile { get; set; }
        public DotaProfile DotaProfile { get; set; }
        public LolProfile LolProfile { get; set; }
        public Language Language { get; set; }
    }
}