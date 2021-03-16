using System;

namespace ESportSchool.Web.ViewModels.Api
{
    public class ScheduleIntervalViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public bool RepeatWeekly { get; set; } = false;
    }
}