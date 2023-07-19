using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.ViewModels;
using System.Security.Claims;

namespace NewsApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager)

        {
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
        }
        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			var checkExistingUser = await _userManager.FindByEmailAsync(model.Email);
			if(checkExistingUser != null)
			{
				TempData["Error"] = "This email address is already in use";
				return View(model);
			}
				
			ApplicationUser user = new()
			{
				Email = model.Email,
				UserName = model.Email,
				FullName = model.FullName,
				PhoneNumber = model.PhoneNumber
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				TempData["Error"] = "Something went wrong";
				return View(model);
			}

			return RedirectToAction("Login","Account");
		}
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> RegisterAdmin(RegisterModel model)
		{
			var checkExistingUser = await _userManager.FindByEmailAsync(model.Email);
			if (checkExistingUser != null)
			{
				TempData["Error"] = "This email address is already in use";
				return View(model);
			}

			ApplicationUser user = new()
			{
				Email = model.Email,
				UserName = model.Email,
				FullName = model.FullName,
				PhoneNumber = model.PhoneNumber
				
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				TempData["Error"] = "Something went wrong";
				return View(model);
			}

			if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

			if (!await _roleManager.RoleExistsAsync(UserRoles.User))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.Admin);
			}
			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.User);
			}
			
			return RedirectToAction("Login", "Account");
		}

		public IActionResult Login(string returnUrl = null)
		{
            ViewData["ReturnUrl"] = returnUrl;
            return View();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is lock because of wrong username or password");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
