using Microsoft.AspNetCore.Mvc;

namespace ESportSchool.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            var endpoints = System.IO.File.ReadAllText(@"./endpoints.txt");

            return Ok(endpoints);
        }
    }
}
