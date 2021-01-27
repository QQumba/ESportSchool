using System;
using System.Text;

namespace ESportSchool.Services.Utils
{
    public class ConfirmationCodeGenerator
    {
        private const int DefaultCodeLength = 6;
        public string GetCode(int length = DefaultCodeLength)
        {
            var sb = new StringBuilder();
            var random = new Random(DateTime.Now.Millisecond * DateTime.Now.Hour);
            for (var i = 0; i < length; i++)
            {
                sb.Append(random.Next(9));
            }
            return sb.ToString();
        }
    }
}