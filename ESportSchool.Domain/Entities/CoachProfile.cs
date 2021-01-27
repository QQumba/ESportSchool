using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ESportSchool.Domain.Entities.GameProfiles;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities
{
    public class CoachProfile : BaseEntity
    {
        public CsgoProfile CsgoProfile { get; set; }
        public DotaProfile DotaProfile { get; set; }
        public LolProfile LolProfile { get; set; }
        public int Rate { get; set; }

        //relations
        [ForeignKey("user")]
        public User User { get; set; }
        public Language Language { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Training> Trainings { get; set; }
        public List<ScheduleInterval> Schedule { get; set; }

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
        
        public bool IsScheduleIntervalAvailable(ScheduleInterval other)
        {
            var intervals = Schedule?.Where(s => s.Start.Date == other.Start.Date);
            if (intervals == null)
            {
                return false;
            }
            
            var orderedIntervals = TrainingsSchedule?.Where(s => s.Start.Date == other.Start.Date);
            if (orderedIntervals?.Where(i => i.IsOverlapped(other)) != null)
            {
                return false;
            }
            
            if(intervals.Any(i => i.Contain(other)))
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            var coach = (CoachProfile) obj;
            return coach.User.Email.Equals(User.Email);
        }

        public override int GetHashCode()
        {
            return User.Email.GetHashCode();
        }
    }
}