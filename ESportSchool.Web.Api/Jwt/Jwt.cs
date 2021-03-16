namespace ESportSchool.Web.Jwt
{
    public class Jwt
    {
        public Jwt(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}