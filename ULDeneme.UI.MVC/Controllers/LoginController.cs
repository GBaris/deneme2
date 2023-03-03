using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.UserViewModels;

namespace ULDeneme.UI.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserBLL _userService;

        public LoginController(IUserBLL userService)
        {
            _userService = userService;
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Login(UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                ResultService<User> userResult = _userService.GetByEMail(model.EMail);
                if (userResult.Errors.Count > 0)
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı");
                    return View(model);
                }
                ResultService<bool> loginResult = _userService.CheckUserForLogin(model.EMail, model.Password);
                if (loginResult.Errors.Count > 0)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı");
                    return View(model);
                }
                else
                {
                    var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Email, userResult.Data.Email),
                        new Claim(ClaimTypes.Name, userResult.Data.NickName),
                        new Claim(ClaimTypes.Role, userResult.Data.UserRole.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, userResult.Data.ID.ToString())
                        };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}