using Microsoft.AspNetCore.Mvc;
using aspnet_mongo.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using aspnet_mongo.Filters;

namespace aspnet_mongo.Controllers
{
    public class LoginController : Controller
    {
        //
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;


        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [RedirectAuthenticatedUser]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [RedirectAuthenticatedUser]
        public async Task<ActionResult> Login([Required][EmailAddress] string email, [Required] string password, bool remember)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser? appUser = await _userManager.FindByEmailAsync(email);

                if(appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password, remember, false);

                    if(result.Succeeded)
                    {
                        return Redirect("/");
                    }
                    ModelState.AddModelError(nameof(email), "Verifique suas credenciais!");
                    return View("Login");
                }
            }
            return View("Login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
