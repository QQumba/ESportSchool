using System.Collections.Generic;
using System.Threading.Tasks;
using ESportSchool.Domain.Entities;
using ESportSchool.Domain.Entities.Mapped;
using ESportSchool.Domain.Repositories;
using ESportSchool.Services.Utils;

namespace ESportSchool.Services.DataAccess
{
    public class TrainingService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly ScheduleService _scheduleService;

        public TrainingService(IUserRepository userRepository,
            ITrainingRepository trainingRepository,
            ITeamRepository teamRepository, ScheduleService scheduleService, ICoachRepository coachRepository)
        {
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _teamRepository = teamRepository;
            _scheduleService = scheduleService;
            _coachRepository = coachRepository;
        }

        public async Task CreateTrainingAsync(Training training, string confirmationLinkBase)
        {
            var sender = new EmailConfirmationSender(new EmailMessageBuilder(training.Coach.User));
            sender.SendMessage(training.Coach.User.Email, confirmationLinkBase + training.Id);
            await _trainingRepository.CreateAsync(training);
        }

        public async Task<Training> GetTraining(int id)
        {
            return await _trainingRepository.GetAsync(id);
        }

        public async Task ConfirmTrainingAsync(int trainingId)
        {
            var training= await _trainingRepository.GetAsync(trainingId);
            training.Accepted = true;

            _trainingRepository.UpdateAsync(training);
            
            foreach (var user in training.Participants)
            {
                var message = $"Training start on {training.Start.ToString()}";
                var sender = new EmailConfirmationSender(new EmailMessageBuilder(user));
                sender.SendMessage(user.Email, message);
            }
        }

        public async Task<List<Coach>> GetAvailableCoachesAsync(CoachFilter filter)
        {
            return await _coachRepository.GetAvailableCoachesAsync(filter); 
        }
    }
}