using Microsoft.AspNetCore.Mvc;
using ULDeneme.ViewModel.UserViewModels;
using ULDeneme.BLL.Abstract;

namespace ULDeneme.UI.MVC.Controllers
{
    public class SignupController : Controller
    {
        private readonly IUserBLL _userService;
        public SignupController(IUserBLL userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(UserCreateVM user)
        {
            var result = _userService.Insert(user);
            if (result.HasError)
            {
                ViewBag.Error = result.HasError;
                return View("Index");
            }

            // set the user's nickname in a session or cookie here

            return RedirectToAction("Index", "Home");
        }
    }
}