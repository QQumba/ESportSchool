using System;
using System.Text;
using ESportSchool.Domain.Entities;

namespace ESportSchool.Services.Utils
{
    public class EmailMessageBuilder
    {
        private readonly User _user;

        public EmailMessageBuilder(User user)
        {
            _user = user;
        }

        public string GetEmailConfirmationMessage()
        {
            return null;
        }
        public string GetAccountDeletionConfirmationMessage()
        {
            return null;
        }
        public string GetTrainingNotificationMessage()
        {
            return null;
        }
    }
}