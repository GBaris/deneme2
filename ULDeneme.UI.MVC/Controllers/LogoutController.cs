﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ULDeneme.UI.MVC.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
