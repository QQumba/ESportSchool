using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Jwt
{
    public abstract class JwtController : ControllerBase
    {
        protected string Email => User.Identity.Name;
    }
}