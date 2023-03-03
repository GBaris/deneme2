using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;

namespace ULDeneme.ViewModel.VocabularyViewModels
{
    public class VocabularyCreateVM
    {
        [Required(ErrorMessage = "Unkown Vocabulary is required")]
        public string UnKVoc { get; set; }
        [Required(ErrorMessage = "Known Vocabulary is required")]
        public string KVoc { get; set; }
        public int SozlukID { get; set; }
    }
}
