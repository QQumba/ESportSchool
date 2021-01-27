using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Repositories;

namespace ESportSchool.Services
{
    public class ScheduleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IScheduleIntervalRepository _scheduleIntervalRepository;

        public ScheduleService(IUserRepository userRepository, IScheduleIntervalRepository scheduleIntervalRepository)
        {
            _userRepository = userRepository;
            _scheduleIntervalRepository = scheduleIntervalRepository;
        }
        
        public List<ScheduleInterval> GetScheduleOrNullIfEmpty(CoachProfile profile)
        {
            return profile.Schedule;
        }

        public async Task CleanupSchedules()
        {
            await _scheduleIntervalRepository.DeleteRangeAsync(await _scheduleIntervalRepository
                .GetOutdatedIntervalsAsync());
        }
    }
}