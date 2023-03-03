using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULDeneme.ViewModel.QuizTestViewModels
{
    public class TestQuestion
    {
        public string Word { get; set; }
        public List<string> Options { get; set; }
        public string SelectedAnswer { get; set; }

    }
}

