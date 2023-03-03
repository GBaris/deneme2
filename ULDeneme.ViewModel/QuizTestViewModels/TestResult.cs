using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULDeneme.ViewModel.QuizTestViewModels
{
    public class TestResult
    {
        public TestQuestion Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect => UserAnswer == CorrectAnswer;
    }
}
