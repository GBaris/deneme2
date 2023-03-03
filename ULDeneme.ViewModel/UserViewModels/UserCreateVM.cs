using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ULDeneme.Model.Enums;

namespace ULDeneme.ViewModel.UserViewModels
{
    public class UserCreateVM
    {
        [Display(Name = "NickName", Prompt = "nickname")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "max 100 chracter", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        [StringLength(100, ErrorMessage = "max 100 chracter", MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Display(Name = "Password", Prompt = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "max 100 chracter", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

    }
}
