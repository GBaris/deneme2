using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;


namespace ULDeneme.ViewModel.VocabularyViewModels
{
    public class VocabularyViewModel
    {
        public string SozlukName { get; set; }
        public string UnKVoc { get; set; }
        public string KVoc { get; set; }
        public int SozlukID { get; set; }
        public Sozluk Sozluk { get; set; }
        public List<Vocabulary> VocabularyList { get; set; }
    }
}