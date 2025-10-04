using IKEA.DAL.Models.Shared;
using IKEA.PL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if(!ModelState.IsValid) return View(registerView);
            var User = new ApplicationUser()
            {
                FirstName= registerView.FirstName,
                LastName    = registerView.LastName,
                UserName = registerView.UserName,
                Email = registerView.Email,
               
            };
           var result= _userManager.CreateAsync(User, registerView.Password).Result;
            if (result.Succeeded) {
              return RedirectToAction(nameof(SignIn));

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(registerView);

        }
        #endregion

        #region Login
        [HttpGet]
        //P@sss0rd
        public async Task<IActionResult> SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInView)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _userManager.FindByEmailAsync(signInView.Email);
            if (user != null)
            {
                var flag = await _userManager.CheckPasswordAsync(user, signInView.Password);
                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signInView.Password, signInView.RememberMe, true);
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed Yet");
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked");
                    }
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                }

            }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                return View(signInView);
            
        }
        #endregion

        #region LogOut
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        #endregion

        #region Reset Password
        #endregion
    }
}
