using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;
using ULDeneme.UI.MVC.Models;
using ULDeneme.ViewModel.SozlukViewModels;

namespace ULDeneme.UI.MVC.Controllers
{
    public class SozlukController : Controller
    {
        private readonly ISozlukBLL _sozlukBLL;
        private readonly ITranslationTypeBLL _translationTypeBLL;


        public SozlukController(ISozlukBLL sozlukBLL, ITranslationTypeBLL translationTypeBLL)
        {
            _translationTypeBLL = translationTypeBLL;
            _sozlukBLL = sozlukBLL;
        }

        public IActionResult Index()
        {
            var translationTypes = _translationTypeBLL.GetAllActive();
            ViewBag.TranslationTypes = new SelectList(translationTypes.Data.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }), "Value", "Text");
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var sozluks = _sozlukBLL.GetSozluksByUserId(userID);
            var userRole = Enum.Parse<UserRole>(User.FindFirst(ClaimTypes.Role)?.Value);

            SozlukCreateVM model = new SozlukCreateVM
            {
                UserID = userID,
                UserRole = userRole
            };
            return View(sozluks.Data);
        }

        public IActionResult Create()
        {
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRole = Enum.Parse<UserRole>(User.FindFirst(ClaimTypes.Role)?.Value);

            SozlukCreateVM model = new SozlukCreateVM
            {
                UserID = userID,
                UserRole = userRole
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SozlukCreateVM sozluk)
        {
            if (ModelState.IsValid)
            {
                ResultService<SozlukCreateVM> result = _sozlukBLL.Insert(sozluk);
                if (result.HasError)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                    return View("Index");
                }
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public IActionResult Update(int id)
        {
            var translationTypes = _translationTypeBLL.GetAllActive();
            ViewBag.TranslationTypes = new SelectList(translationTypes.Data.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }), "Value", "Text");
            ResultService<Sozluk> result = _sozlukBLL.GetSozlukById(id);
            if (result.HasError)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("ID hatası", x.ErrorMessage));
                return View("Error", new ErrorViewModel { RequestId = result.Errors[0].ErrorMessage });
            }

            SozlukEditVM model = new SozlukEditVM
            {
                ID = result.Data.ID,
                Name = result.Data.Name,
                Description = result.Data.Description,
                Theme = result.Data.Theme,
                TranslationTypeID= result.Data.TranslationTypeID,
                ModifiedDate= result.Data.ModifiedDate,
            };
            return PartialView("Update", model);
        }
        [HttpPost]
        public IActionResult Update(SozlukEditVM sozlukEditVM)
        {
            if (ModelState.IsValid)
            {
                ResultService<SozlukEditVM> result = _sozlukBLL.Update(sozlukEditVM);
                if (result.HasError)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                    return View("Index");
                }
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public IActionResult Delete(int id)
        {
            ResultService<Sozluk> result = _sozlukBLL.GetSozlukById(id);
            if (result.HasError)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                return View("Error", new ErrorViewModel { RequestId = result.Errors[0].ErrorMessage });
            }

            return PartialView("Delete", result.Data);
        }
        [HttpPost]
        public JsonResult DeleteType(int id)
        {
            var result = _sozlukBLL.Delete(id);
            if (result.IsSuccess)
            {
                return Json(new { result = result.IsSuccess });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        public ActionResult UsersSozluks()
        {
            var result = _sozlukBLL.GetAllActive();
            if (result.IsSuccess)
            {
                var viewModel = new UsersSozluksViewModel();
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                viewModel.Sozluks = result.Data.Where(x => x.UserID == userID).ToList();

                var translationTypes = _translationTypeBLL.GetAllActive();
                ViewBag.TranslationTypes = new SelectList(translationTypes.Data.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }), "Value", "Text");
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult UsersSozluks(string TranslationTypeID, int ThemeID, int UserID)
        {
            ResultService<List<Sozluk>> result;
            if (ThemeID == 0)
            {
                result = _sozlukBLL.GetSozluksByTranslationTypeID(Convert.ToInt32(TranslationTypeID), UserID);
            }
            else if (TranslationTypeID == null)
            {
                result = _sozlukBLL.GetSozluksByThemes(ThemeID, UserID);
            }
            else
            {
                result = _sozlukBLL.GetSozluksByThemesbyUserIDbyTranslationType(ThemeID, Convert.ToInt32(TranslationTypeID), UserID);
            }
            var translationTypes = _translationTypeBLL.GetAllActive();
            ViewBag.TranslationTypes = new SelectList(translationTypes.Data, "ID", "Name");
            ViewBag.UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (result.IsSuccess)
            {
                UsersSozluksViewModel viewModel = new UsersSozluksViewModel();
                viewModel.TranslationTypeID = Convert.ToInt32(TranslationTypeID);
                viewModel.ThemeID = ThemeID;
                viewModel.Sozluks = result.Data;
                return View(viewModel);
            }
            return View();
        }

        public ActionResult AdminsSozluks(int ThemeID)
        {
            var result = _sozlukBLL.GetSozlukByThemesByAdmin(ThemeID);
            var translationTypes = _translationTypeBLL.GetAllActive();
            ViewBag.TranslationTypes = new SelectList(translationTypes.Data, "ID", "Name");
            if (result.IsSuccess)
            {
                UsersSozluksViewModel viewModel = new UsersSozluksViewModel();
                viewModel.ThemeID = ThemeID;
                viewModel.Sozluks = result.Data;
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdminsSozluks(int ThemeID, int TranslationTypeID)
        {
            var result = _sozlukBLL.GetSozluksByThemesByTranslationTypeByAdmin(ThemeID, TranslationTypeID);
            var translationTypes = _translationTypeBLL.GetAllActive();
            ViewBag.TranslationTypes = new SelectList(translationTypes.Data, "ID", "Name");
            if (result.IsSuccess)
            {
                UsersSozluksViewModel viewModel = new UsersSozluksViewModel();
                viewModel.ThemeID = ThemeID;
                viewModel.Sozluks = result.Data;
                return View(viewModel);
            }
            return View();
        }


    }
}