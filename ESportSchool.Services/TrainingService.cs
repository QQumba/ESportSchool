using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ESportSchool.Domain.Constants;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.NotMapped;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services.Utils;

namespace ESportSchool.Services
{
    public class TrainingService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICoachProfileRepository _coachProfileRepository;
        private readonly ScheduleService _scheduleService;

        public TrainingService(IUserRepository userRepository,
            ITrainingRepository trainingRepository,
            ITeamRepository teamRepository, ScheduleService scheduleService, ICoachProfileRepository coachProfileRepository)
        {
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _teamRepository = teamRepository;
            _scheduleService = scheduleService;
            _coachProfileRepository = coachProfileRepository;
        }

        public async Task CreateTrainingAsync(Training training, string confirmationLinkBase)
        {
            var sender = new EmailConfirmationSender(new EmailMessageBuilder(training.Coach.User));
            sender.SendMessage(training.Coach.User.Email, confirmationLinkBase + training.Id);
            await _trainingRepository.AddAsync(training);
        }

        public async Task<Training> GetTraining(int id)
        {
            return await _trainingRepository.GetByIdAsync(id);
        }

        public async Task ConfirmTrainingAsync(int trainingId)
        {
            var training= await _trainingRepository.GetByIdAsync(trainingId);
            training.Accepted = true;

            await _trainingRepository.UpdateAsync(training);
            
            foreach (var user in training.Participants)
            {
                var message = $"Training start on {training.Start.ToString()}";
                var sender = new EmailConfirmationSender(new EmailMessageBuilder(user));
                sender.SendMessage(user.Email, message);
            }
        }

        public async Task<List<CoachProfile>> GetAvailableCoachesAsync(CoachFilter filter)
        {
            return await _coachProfileRepository.GetAvailableCoachesAsync(filter); 
        }
    }
}