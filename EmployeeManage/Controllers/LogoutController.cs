using EmployeeManage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class LogoutController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<CustomRegisterUser> _signinManager;

        public LogoutController(IHttpContextAccessor httpContextAccessor , SignInManager<CustomRegisterUser> signInManager)
        {
            _signinManager = signInManager;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost]
        public async Task<IActionResult> Signout()
        {
        await _signinManager.SignOutAsync();

            return RedirectToAction("LoginUser", "Login");
        }
    }
}