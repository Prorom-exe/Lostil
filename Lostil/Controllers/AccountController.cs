using Lostil.Data;
using Lostil.Data.Models;
using Lostil.Data.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        AppDbContext db;
        public AccountController(UserManager<User> userManager, SignInManager<User> singInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            db = appDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    
                    // установка куки
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {


                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("LK", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public  async Task<ViewResult> LK(string tovarName, string status)
        {
            var lkUser = db.Orders.AsQueryable();

            if(tovarName != null)
            {
                lkUser = lkUser.Where(x => x.OrderName.Contains(tovarName));
            }
            if(status != null)
            {
                lkUser = lkUser.Where(x => x.Status.Contains(status));
            }
            if (!User.IsInRole("Admin"))
            {
                lkUser = lkUser.Where(u => u.UserName == User.Identity.Name);

            }
            var result = lkUser.ToList().GroupBy(p => p.Date).ToList();
            var model = new LKViewModel
            {
                Orders = result,
                Products = db.Products.Distinct().Select(x => x.ProductName).ToList(),
            };
            return View(model);
             
        }
        [Authorize(Roles ="Admin,Moderator")]
        public ViewResult LK_Admin()
        {

            
            var lkUser = db.Orders.ToList();
            return View(lkUser);

        }





    }
}
