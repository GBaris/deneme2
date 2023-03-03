using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;

namespace ULDeneme.ViewModel.SozlukViewModels
{
    public class SozlukCreateVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters", MinimumLength = 2)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int TranslationTypeID { get; set; }
        public Theme Theme { get; set; }
        public int UserID { get; set; }
        public UserRole UserRole { get; set; }
    }
}
