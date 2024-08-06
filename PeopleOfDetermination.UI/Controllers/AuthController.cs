//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB Guidegrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeopleOfDetermination.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            //return Redirect("https://preprod.kfu.edu.sa/Services/account/LogOffExt");
            return Redirect(_configuration.GetSection("FormsAuthentication:Logout").Value);
            //return RedirectToAction("Index", "Services");
        }

        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Services");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
