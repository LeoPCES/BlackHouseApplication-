﻿using BlackHouseApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlackHouseApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName); // tentar localizar o usuário pelo nome

            if (user != null)
            {
       
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false); 
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return Redirect(loginVM.ReturnUrl);
            }
            // se o usuario for NULL
            ModelState.AddModelError("", "Falha ao ao logar!");
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // remove todos os valores (conteudo) dos objetos da sessao
            HttpContext.Session.Clear();
            HttpContext.User = null;

            await _signInManager.SignOutAsync(); // faz o logout do usuario
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
