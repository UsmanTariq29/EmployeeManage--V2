using EmployeeManage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EmployeeManage.ViewModels.Request;

namespace EmployeeManage.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomRegisterUser> _userManager;
        private readonly SignInManager<CustomRegisterUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<CustomRegisterUser> userManager, SignInManager<CustomRegisterUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomRegisterUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UserGuid = Guid.NewGuid().ToString(),
                    BranchGuid = model.BranchGUID,
                    CompanyGuid = model.CompanyGUID,
                    // Set other properties like BranchGuid and CompanyGuid
                
                };
                

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //var roleName = "UserRole";

                    // IdentityRole role = await _roleManager.FindByNameAsync(model.Role);
                    
                    
                        await _roleManager.CreateAsync();
                    

                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Sign in the user after registration if desired
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("LoginUser", "Login"); // Redirect to home page
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
