using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;

namespace ULDeneme.ViewModel.SozlukViewModels
{
    public class SozlukEditVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TranslationTypeID { get; set; }
        public Theme Theme { get; set; }
        public string? Description { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

    }
}
