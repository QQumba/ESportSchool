using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ESportSchool.Domain.Constants;

namespace ESportSchool.Domain.Entities.Mapped
{
    public class User : BaseEntity
    {
        //basic info
        public string Email { get; set; }
        public string Login { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        //balance data
        public Currency DisplayedCurrency { get; set; } = Currency.USD;
        public decimal Balance { get; set; } = 0;
        
        //administration
        public string Role { get; set; } = UserRole.User;
        
        //confirmation
        public string ConfirmationCode { get; set; }
        public bool HasConfirmedEmail { get; set; } = false;

        //relations 
        public Coach Coach { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Guide> Guides { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Review> Reviews { get; set; }
        public virtual Team Team { get; set; }
        public bool IsTeamLeader { get; set; }
        
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
