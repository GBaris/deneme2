using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Core.Entity;
using ULDeneme.Model.Enums;

namespace ULDeneme.Model.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            IsActive = true;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<TranslationType> TranslationTypes { get; set; }
        public ICollection<Sozluk> Sozluks { get; set; }
    }
}
