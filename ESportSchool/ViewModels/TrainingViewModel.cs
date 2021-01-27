using ESportSchool.Domain.Entities;

namespace ESportSchool.Web.ViewModels
{
    public class TrainingViewModel
    {
        public CoachProfile CoachProfile { get; set; }
        public ScheduleInterval Interval { get; set; }
    }
}