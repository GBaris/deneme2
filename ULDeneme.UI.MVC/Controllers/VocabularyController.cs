using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.BLL.Concrete;
using ULDeneme.Model.Entities;
using ULDeneme.UI.MVC.Models;
using System.Linq;
using System.Security.Claims;
using ULDeneme.ViewModel.VocabularyViewModels;
using ULDeneme.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ULDeneme.DAL.Concrete.Context;
using System;

namespace ULDeneme.UI.MVC.Controllers
{
    public class VocabularyController : Controller
    {
        private readonly IVocabularyBLL _vocabularyBLL;
        private readonly ISozlukBLL _sozlukBLL;
        private readonly ULDenemeDbContext _context;

        public VocabularyController(IVocabularyBLL vocabularyBLL, ISozlukBLL sozlukBLL)
        {
            _vocabularyBLL = vocabularyBLL;
            _sozlukBLL = sozlukBLL;
        }

        public IActionResult Index(int sozlukID)
        {
            ViewBag.SozlukID = sozlukID;
            var sozlukResult = _sozlukBLL.GetSozlukById(sozlukID);
            ViewBag.Title = sozlukResult.Data.Name;
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!sozlukResult.IsSuccess || sozlukResult.Data.UserID != userID)
            {
                return NotFound();
            }

            var vocabularyResult = _vocabularyBLL.GetVocBySozlukID(sozlukID);

            if (!vocabularyResult.IsSuccess)
            {

                ViewBag.NoVocabMessage = "Henüz kelime eklenmemiş.";
                return View(new List<Vocabulary>());
            }

            var viewModel = new VocabularyViewModel
            {
                SozlukName = sozlukResult.Data.Name,
                SozlukID = sozlukID,
                VocabularyList = vocabularyResult.Data
            };

            return View(viewModel);
        }

        public IActionResult AdminSozluksVoc(int sozlukID)
        {
            ViewBag.SozlukID = sozlukID;
            var sozlukResult = _sozlukBLL.GetSozlukById(sozlukID);

            if (!sozlukResult.IsSuccess)
            {
                return NotFound("Sözlük bulunamadı");
            }

            var vocabularyResult = _vocabularyBLL.GetVocBySozlukID(sozlukID);

            if (!vocabularyResult.IsSuccess)
            {

                ViewBag.NoVocabMessage = "Henüz kelime eklenmemiş.";
                return View(new List<Vocabulary>());
            }

            var viewModel = new VocabularyViewModel
            {
                SozlukName = sozlukResult.Data.Name,
                SozlukID = sozlukID,
                VocabularyList = vocabularyResult.Data
            };

            return View(viewModel);
        }

        public IActionResult Create(int sozlukID)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VocabularyCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var newVoc = new VocabularyCreateVM
            {
                SozlukID = viewModel.SozlukID,
                UnKVoc = viewModel.UnKVoc,
                KVoc = viewModel.KVoc
            };

            var result = _vocabularyBLL.Insert(newVoc);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Index", new { sozlukID = viewModel.SozlukID });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vocabularyResult = _vocabularyBLL.GetVocByID(id);
            if (vocabularyResult.HasError)
            {
                // handle error case
                return View("Error");
            }

            var vocabulary = vocabularyResult.Data;

            var viewModel = new VocabularyUpdateVM
            {
                ID = vocabulary.ID,
                UnKVoc = vocabulary.UnKVoc,
                KVoc = vocabulary.KVoc,
                SozlukID = vocabulary.SozlukID,
                //Sozluk = vocabulary.Sozluk,
                ModifiedDate = DateTime.Now
            };

            return PartialView("Update", viewModel);
        }

        [HttpPost]
        public IActionResult Update(VocabularyUpdateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                // handle validation errors
                return View(viewModel);
            }

            var result = _vocabularyBLL.Update(viewModel);
            if (result.HasError)
            {
                // handle error case
                return View("Error");
            }
            return RedirectToAction("Index", new { sozlukID = viewModel.SozlukID });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var vocabulary = _vocabularyBLL.GetVocByID(id);
            var sozID = vocabulary.Data.SozlukID;
            var result = _vocabularyBLL.Delete(id);
            if (result.HasError)
            {
                // handle error case
                return View("Error");
            }
            return RedirectToAction("Index", new { sozlukID = sozID });
        }
    }
}