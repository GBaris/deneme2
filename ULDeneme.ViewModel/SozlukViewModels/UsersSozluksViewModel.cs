using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;

namespace ULDeneme.ViewModel.SozlukViewModels
{
    public class UsersSozluksViewModel
    {
        public int TranslationTypeID { get; set; }
        public TranslationType TranslationType { get; set; }
        public List<Sozluk> Sozluks { get; set; }
        public int ThemeID { get; set; }
        public int UserID { get; set; }
    }
}
