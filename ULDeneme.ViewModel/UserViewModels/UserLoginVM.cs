using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ULDeneme.ViewModel.UserViewModels
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} exception")]
        [Display(Name = "Email Address", Prompt = "e-mail address")]
        [StringLength(100, ErrorMessage = "max 100 character", MinimumLength = 10)]
        public string EMail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "max 20 character", MinimumLength = 3)]
        public string Password { get; set; }
    }
}
