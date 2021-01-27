using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class User : BaseEntity
    {
        //basic info
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string About { get; set; }
        public string ImagePath { get; set; }        

        //balance data
        public Currency DisplayedCurrency { get; set; } = Currency.USD;
        public float Balance { get; set; } = 0;
        
        //administration
        public string Role { get; set; } = UserRole.User.ToString();
        
        //confirmation
        public string ConfirmationCode { get; set; }
        public bool HasConfirmedEmail { get; set; } = false;

        //relations
        public CoachProfile CoachProfile { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Guide> Guides { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Review> Reviews { get; set; }
        public Team Team { get; set; }
        
        [NotMapped]
        public List<ScheduleInterval> TrainingsSchedule
        {
            get
            {
                var intervals = Trainings?.Where(i => i.Start > DateTime.Now).Select(i => new ScheduleInterval()
                {
                    Start = i.Start,
                    Duration = i.Duration,
                }).ToList();

                return intervals;
            }
        }
    }
}
