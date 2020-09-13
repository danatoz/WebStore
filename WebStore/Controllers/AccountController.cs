using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL;
using WebStore.Models.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView loginView)
        {
            if (!ModelState.IsValid)
                return View();

            var loginResult = await _signInManager.PasswordSignInAsync(
                loginView.UserName,
                loginView.Password,
                loginView.RememberMe,
                lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Вход невозможен");
                return View(loginView);
            }

            if(Url.IsLocalUrl(loginView.ReturnUrl))
                return RedirectToAction(loginView.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserView());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User { UserName = model.UserName, Email = model.Email }; //создаем сущность пользователь
            var createResult = await _userManager.CreateAsync(user, model.Password); //используем менеджер для создания
            if (!createResult.Succeeded)
            {
                foreach (var identityError in createResult.Errors) //выводим ошибки
                {
                    ModelState.AddModelError("", identityError.Description);
                    return View(model);
                }

            }
            await _signInManager.SignInAsync(user, false); //если успешно -производим логин
            return RedirectToAction("Index", "Home");
        }
    }
}
