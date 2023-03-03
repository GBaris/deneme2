using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Core.Entity;
using ULDeneme.Model.Enums;

namespace ULDeneme.Model.Entities
{
    public class Sozluk : BaseEntity
    {
        public Sozluk()
        {
            IsActive = true;
        }
        public int TranslationTypeID { get; set; }
        public TranslationType TranslationType { get; set; }
        public string Name { get; set; }
        public Theme Theme { get; set; }
        public string? Description { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
