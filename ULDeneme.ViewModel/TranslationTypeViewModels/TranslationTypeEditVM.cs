using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ULDeneme.Model.Enums;

namespace ULDeneme.ViewModel.TranslationTypeViewModels
{
    public class TranslationTypeEditVM
    {
        public int ID { get; set; }
        private string _knownLang;
        [Required(ErrorMessage = "Known language is required")]
        [StringLength(50, ErrorMessage = "Known language cannot be more than 50 characters", MinimumLength = 2)]
        [Display(Name = "Known Language")]
        public string KnownLang
        {
            get { return _knownLang; }
            set { _knownLang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); }
        }

        private string _unknownLang;
        [Required(ErrorMessage = "Unknown language is required")]
        [StringLength(50, ErrorMessage = "Unknown language cannot be more than 50 characters", MinimumLength = 2)]
        [Display(Name = "Unknown Language")]
        public string UnknownLang
        {
            get { return _unknownLang; }
            set { _unknownLang = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); }
        }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
