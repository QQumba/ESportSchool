using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;

namespace ESportSchool.Web.ViewModels
{
    public class TrainingViewModel
    {
        public Coach Coach { get; set; }
        public ScheduleInterval Interval { get; set; }
    }
}