using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ULDeneme.DAL.Concrete.Context;
using System.Linq;
using ULDeneme.ViewModel.QuizTestViewModels;
using ULDeneme.BLL.Abstract;

namespace ULDeneme.UI.MVC.Controllers
{
    public class QuizTestController : Controller
    {
        private readonly ISozlukBLL _sozlukBLL;
        private readonly ULDenemeDbContext _context;

        public QuizTestController(ULDenemeDbContext context, ISozlukBLL sozlukBLL)
        {
            _context = context;
            _sozlukBLL= sozlukBLL;
        }

        [HttpGet]
        public IActionResult Learn(int sozlukID)
        {
            var vocabularyList = _context.Vocabularies
                .Where(v => v.SozlukID == sozlukID && v.IsActive) // sadece aktif kayıtları getiriyoruz
                .ToList();

            vocabularyList.Shuffle();

            var testResult = new List<TestResult>();
            var random = new Random();

            ViewBag.SozlukAdi = _sozlukBLL.GetSozlukById(sozlukID).Data.Name;

            ViewBag.SozlukID = sozlukID;

            foreach (var vocabulary in vocabularyList)
            {
                var options = _context.Vocabularies
                    .Where(v => v.SozlukID == sozlukID && v.IsActive && v.ID != vocabulary.ID) // sadece aktif ve farklı kayıtları getiriyoruz
                    .ToList();

                // Eğer seçenekler arasında zaten bilinen kelime yoksa,
                // rastgele bir seçeneği doğru cevap olarak ekle

                options = options.OrderBy(o => random.Next()).Take(4).ToList();
                if (options.Count<4)
                {
                    return View("Learn", testResult);
                }

                if (!options.Any(o => o.KVoc == vocabulary.KVoc))
                {
                    options.Insert(0, vocabulary);
                    options.RemoveAt(1); ;
                }

                var question = new TestQuestion
                {
                    Word = vocabulary.UnKVoc,
                    Options = options.Select(o => o.KVoc).ToList()
                };

                question.Options.Shuffle();

                testResult.Add(new TestResult
                {
                    Question = question,
                    CorrectAnswer = vocabulary.KVoc
                });
            }
            return View("Learn", testResult);
        }

        [HttpPost]
        public IActionResult Learn(List<TestResult> testResultList, int sozlukID)
        {
            ViewBag.SozlukID= sozlukID;
            var incorrectAnswers = testResultList.Where(r => r.IsCorrect == false).ToList();
            // Sonuçları ViewBag'e kaydetmek için örnek kod
            var testResultViewBag = new List<TestResult>();
            foreach (var result in testResultList)
            {
                var testResult = new TestResult
                {
                    Question = result.Question,
                    UserAnswer = result.UserAnswer,
                    CorrectAnswer = result.CorrectAnswer
                };
                testResultViewBag.Add(testResult);
            }
            ViewBag.TestResultList = testResultViewBag;

            return View("LearnResult", incorrectAnswers);
        }

    }
}