using System.Collections.Generic;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Web.ViewModels
{
    public class CoachViewModel
    {
        public List<GameProfile> GameProfiles { get; set; }
        public Language Language { get; set; }
    }
}