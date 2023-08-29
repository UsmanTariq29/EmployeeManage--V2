using EmployeeManage.Models;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<CustomRegisterUser> _signinmanager;
        public LoginController(SignInManager<CustomRegisterUser> signinmanager)
        {
            _signinmanager = signinmanager;
        }
        [HttpGet]
        public ViewResult LoginUser()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserRequestVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinmanager.PasswordSignInAsync(model.UserName,model.Password,
                    model.RememberMe,false);
                if (result.Succeeded)
                {

                    //this.HttpContext.Session.SetString("Username", model.UserName);
                    //this.HttpContext.Session.SetString("UserGUID", validate.UserGuid);
                    //this.HttpContext.Session.SetString("companyGUID", validate.CompanyGuid);
                    //this.HttpContext.Session.SetString("branchGUID", validate.BranchGuid);
                    return RedirectToAction("PurchasePayment", "Purchase");
                }

                    ModelState.AddModelError(string.Empty,"invalid Credentials");
            }
            return View(model);
        }
    }
}


//        var validate = await _user.user(user.Email, user.Password);


//        if (validate != null)
//        {

//            if (validate.Passwordexp <= System.DateTime.Today)
//            {
//                //throw new Exception("Password Expires");                    
//            }


//            this.HttpContext.Session.SetString("Username", validate.UserName);
//            this.HttpContext.Session.SetString("UserGUID", validate.UserGuid);
//            this.HttpContext.Session.SetString("companyGUID", validate.CompanyGuid);
//            this.HttpContext.Session.SetString("branchGUID", validate.BranchGuid);

//            //return RedirectToAction("BarcodeSearch", "Barcode");
//            return RedirectToAction("PurchasePayment", "Purchase");
//        }
//        else
//            return RedirectToAction("Index");
//    }