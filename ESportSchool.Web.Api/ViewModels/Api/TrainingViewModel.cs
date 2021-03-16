using System;

namespace ESportSchool.Web.ViewModels.Api
{
    public class TrainingViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public byte Duration { get; set; }
        public bool Accepted { get; set; }
    }
}