using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.DAL;
using ULDeneme.Model;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;
using ULDeneme.UI.MVC.Models;
using ULDeneme.ViewModel.TranslationTypeViewModels;

namespace ULDeneme.UI.MVC.Controllers
{
    public class TranslationTypeController : Controller
    {
        private readonly ITranslationTypeBLL _translationTypeBLL;

        public TranslationTypeController(ITranslationTypeBLL translationTypeBLL)
        {
            _translationTypeBLL = translationTypeBLL;        
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            ResultService<List<TranslationType>> result = _translationTypeBLL.GetTypesByUserID(userID);
            if (result.Data.Count == 0)
            {
                return View("EmmptyTranslationTypeList");
            }
            else
            {
                if (ControllerContext.RouteData.Values["action"].ToString() == "Update")
                {
                    ViewBag.ViewName = "Update";
                }
                else
                {
                    ViewBag.ViewName = "Index";
                }
                return View(result.Data);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRole = Enum.Parse<UserRole>(User.FindFirst(ClaimTypes.Role)?.Value);

            TranslationTypeCreateVM model = new TranslationTypeCreateVM
            {
                UserID = userID,
                UserRole = userRole
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(TranslationTypeCreateVM translationType)
        {
            if (ModelState.IsValid)
            {
                ResultService<TranslationTypeCreateVM> result = _translationTypeBLL.Insert(translationType);
                if (result.HasError)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                    return View("Index");
                }
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ResultService<TranslationType> result = _translationTypeBLL.GetTypeById(id);
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
            var result = _translationTypeBLL.Delete(id);
            if (result.IsSuccess)
            {
                return Json(new { result = result.IsSuccess });
            }
            else
            {
                return Json(new { result = false });
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePartial(int id)
        {
            ResultService<TranslationType> result = _translationTypeBLL.GetTypeById(id);
            if (result.HasError)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                return View("Error", new ErrorViewModel { RequestId = result.Errors[0].ErrorMessage });
            }

            TranslationTypeEditVM model = new TranslationTypeEditVM
            {
                ID = result.Data.ID,
                KnownLang = result.Data.KnownLang,
                UnknownLang = result.Data.UnknownLang,
                ModifiedDate= result.Data.ModifiedDate,
            };

            return PartialView("Update", model);
        }
        [HttpPost]
        public IActionResult Update(TranslationTypeEditVM translationType)
        {
            if (ModelState.IsValid)
            {
                ResultService<TranslationTypeEditVM> result = _translationTypeBLL.Update(translationType);
                if (result.HasError)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.ErrorMessage));
                    return View("Index");
                }
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
