using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Core.Entity;

namespace ULDeneme.Model.Entities
{
    public class Vocabulary : BaseEntity
    {
        public Vocabulary()
        {
            IsActive= true;
        }
        public string UnKVoc { get; set; }
        public string KVoc { get; set; }
        public int SozlukID { get; set; }
        public Sozluk Sozluk { get; set; }
    }
}
